using System;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Tools.Services.EventActions {

    public abstract class ActionOnEvent: InjectedMonoBehaviour {
        
        [field:SerializeField]
        public EventKey EventType { get; private set; }

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        protected new virtual void Awake() {

            base.Awake();

            if (EventType != EventKey.Unknown) {

                events?.Add(EventType, OnEvent);

            }

        }

        protected virtual void OnDestroy() {

            if (EventType != EventKey.Unknown) {

                events?.Remove(EventType, OnEvent);

            }

        }

        #endregion

        protected abstract void OnEvent(EventArgs e);

    }

}