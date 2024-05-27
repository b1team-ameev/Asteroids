using System;
using System.Collections.Generic;
using UnityEngine;
using HGS.Enums;

namespace HGS.Tools.Services.ServiceEvents {

    public class Events: MonoBehaviour {

        private readonly Dictionary<string, Delegate> dEvents = new ();
        private readonly Dictionary<string, List<EventArgs>> dDelayedExecutions = new ();
        private readonly Dictionary<EventKey, EventArgs> dSingleEvents = new ();

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            DontDestroyOnLoad(gameObject);

        }

        private void Update() {

            lock(dDelayedExecutions) {

                foreach(var eventPair in dDelayedExecutions) {

                    foreach(var evemtArgs in eventPair.Value.OrEmptyIfNull()) {

                        Raise(eventPair.Key, evemtArgs);

                    }

                }
                
                dDelayedExecutions.Clear();

            }

        }

        private void OnDestroy() {

            lock(dEvents) {

                foreach(var eventPair in dEvents) {

                    if (eventPair.Value != null) {

                        Delegate d = eventPair.Value;

                        foreach(var delegateItem in d.GetInvocationList()) {
                            
                            d = Delegate.Remove(d, delegateItem);

                        }

                    }

                }
                
                dEvents.Clear();

            }

            lock(dSingleEvents) {

                dSingleEvents.Clear();

            }

        }

        #endregion

        private static string GetName(object obj, EventKey eventKey) {

            return eventKey.ToString() + (obj == null ? "" : "_" + obj.GetHashCode().ToString());

        }

        public void Add(object obj, EventKey eventKey, Action<EventArgs> action) {

            Delegate handler = action;
            string name = GetName(obj, eventKey);

            lock(dEvents) {

                Delegate d;

                dEvents.TryGetValue(name, out d);
                dEvents[name] = Delegate.Combine(d, handler);

            }

            lock(dSingleEvents) {

                if (dSingleEvents.ContainsKey(eventKey) && handler != null) {

                    handler.DynamicInvoke(new object[] { dSingleEvents[eventKey] });

                }

            }

        }

        public void Add(EventKey eventKey, Action<EventArgs> action) {

            Add(null, eventKey, action);

        }

        public void Remove(object obj, EventKey eventKey, Action<EventArgs> action) {

            Delegate handler = action;
            string name = GetName(obj, eventKey);

            lock(dEvents) {

                Delegate d;

                if (dEvents.TryGetValue(name, out d)) {

                    d = Delegate.Remove(d, handler);

                    if (d != null) {

                        dEvents[name] = d;

                    }
                    else {

                        dEvents.Remove(name);

                    }

                }

            }

        }

        public void Remove(EventKey eventKey, Action<EventArgs> action) {

            Remove(null, eventKey, action);

        }

        private void Raise(object obj, EventKey eventKey, EventArgs e, bool isSingleEvent = false) {

            string name = GetName(obj, eventKey);

            Raise(name, e);
            
            // сохраняем, чтобы повторить позже, для добавляемых подписчиков, так как предполагается, что
            // событие будет вызвано единожды и его нельзя пропустить
            if (isSingleEvent) {

                lock(dSingleEvents) {

                    if (dSingleEvents.ContainsKey(eventKey)) {

                        dSingleEvents[eventKey] = e;

                    }
                    else {

                        dSingleEvents.Add(eventKey, e);

                    }

                }

            }

        }

        private void Raise(string name, EventArgs e) {

            Delegate d;
            
            lock(dEvents) {

                dEvents.TryGetValue(name, out d);

            }

            if (d != null) {

                d.DynamicInvoke(new object[] { e });

            }

        }

        public void Raise(EventKey eventKey, EventArgs e, bool isSingleEvent = false) {

            Raise(null, eventKey, e, isSingleEvent);

        }

        public void Raise(EventKey eventKey, string e) {

            Raise(eventKey, new UniversalEventArgs<string>(e));

        }

        public void Raise(EventKey eventKey, bool isSingleEvent) {

            Raise(eventKey, new EventArgs(), isSingleEvent);

        }

        public void Raise(EventKey eventKey) {

            Raise(eventKey, new EventArgs());

        }

        public void Raise(object obj, EventKey eventKey) {

            Raise(obj, eventKey, new EventArgs());

        }

        public void Raise(object obj, EventKey eventKey, EventArgs e) {

            Raise(obj, eventKey, e, false);

        }

        public void RaiseDelayed(object obj, EventKey eventKey, EventArgs e) {

            string name = GetName(obj, eventKey);

            lock(dDelayedExecutions) {

                if (!dDelayedExecutions.ContainsKey(name)) {

                    dDelayedExecutions.Add(name, new ());

                }

                dDelayedExecutions[name].Add(e);

            }

        }

        public void RaiseDelayed(EventKey eventKey, EventArgs e) {

            RaiseDelayed(null, eventKey, e);

        }

        public void RaiseDelayed(EventKey eventKey) {

            RaiseDelayed(null, eventKey, new EventArgs());

        }

        public void RaiseDelayed(object obj, EventKey eventKey) {

            RaiseDelayed(obj, eventKey, new EventArgs());

        }

    }

}