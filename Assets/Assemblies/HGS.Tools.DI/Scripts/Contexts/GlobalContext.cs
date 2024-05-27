using System.Collections.Generic;

namespace HGS.Tools.DI.Contexts {

    internal static class GlobalContext {

        private static readonly List<IContext> contexts = new List<IContext>();

        public static void AddContext(IContext context) {

            lock(contexts) {

                contexts.Add(context);                

            }

        }

        public static void RemoveContext(IContext context) {

            lock(contexts) {

                contexts.Remove(context);                

            }

        }

        public static T Resolve<T>() where T: class {

            T value = default;

            lock(contexts) {

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

    }

}
