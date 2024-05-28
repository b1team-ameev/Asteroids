using System;
using System.Collections.Generic;
using HGS.Tools.DI.Injection;

namespace HGS.Tools.DI.Contexts {

    internal static class GlobalContext {

        private static readonly object lockObject = new object();

        private static readonly List<IContext> contexts = new List<IContext>();
        private static readonly Dictionary<Type, List<WeakReference>> unresolvedLinks = new ();

        public static void AddContext(IContext context) {

            lock(lockObject) {

                contexts.Add(context);                

            }

        }

        public static void RemoveContext(IContext context) {

            lock(lockObject) {

                contexts.Remove(context);     

                if (contexts.Count == 0) {

                    foreach(var list in unresolvedLinks) {

                        list.Value?.Clear();

                    }

                    unresolvedLinks.Clear();

                }           

            }

        }

        public static T Resolve<T>() where T: class {

            T value = default;

            lock(lockObject) {

                foreach(IContext context in contexts) {

                    if (context != null) {

                        value = context.Resolve<T>();

                    }

                    if (value != default) {

                        break;

                    }

                }

            }

            return value;

        }

        // Injector - GlobalContext - IContext - DIContainer
        public static T ResolveOrRemember<T>(object iObject) where T: class {

            T value = Resolve<T>();

            if (value == default) {

                Type type = typeof(T);

                lock(lockObject) {

                    if (!unresolvedLinks.ContainsKey(type)) {

                        unresolvedLinks.Add(type, new ());

                    }

                    unresolvedLinks[type].Add(new WeakReference(iObject));

                }

            }

            return value;

        }

        // DIContainer - IContext - GlobalContext - Injector
        // по сути вырожденный случай; закрывает case, когда на момент обращения к Injector в binds DIContainer не было <T>
        public static void OnBind<T>() {

            List<WeakReference> links = null;
            Type type = typeof(T);
            
            lock(lockObject) {

                if (unresolvedLinks.ContainsKey(type)) {

                    links = unresolvedLinks[type];
                    unresolvedLinks.Remove(type);

                }

            }

            if (links != null) {

                foreach(var link in links) {

                    Injector.Inject(link?.Target);

                }

            }

        }

    }

}
