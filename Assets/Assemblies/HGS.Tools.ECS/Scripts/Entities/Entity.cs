using System;
using System.Collections.Generic;
using HGS.Tools.ECS.Components;

namespace HGS.Tools.ECS.Entities {

    public class Entity: IEntity, IDisposable {

        private readonly Dictionary<Type, IComponent> components = new ();
        
        public object IdObject { get; private set; } 
        public void SetIdObject(object idObject) {

            IdObject = idObject;
            
        }

        public T GetComponent<T>() where T: IComponent {
            
            Type componentType = typeof(T);

            lock(components) {

                if (components.ContainsKey(componentType)) {

                    return (T)components[componentType];

                }
                else if (componentType.IsInterface) {

                    foreach(var componentInfo in components) {

                        if (componentInfo.Value is T) {

                            return (T)componentInfo.Value;

                        }

                    }

                }
                else {

                    foreach(var componentInfo in components) {

                        if (componentInfo.Key.IsSubclassOf(componentType)) {

                            return (T)componentInfo.Value;

                        }

                    }

                }

            }

            return default;

        }

        public T AddComponent<T>() where T: class, IComponent {
            
            Type componentType = typeof(T);
            return AddComponent(componentType.GetConstructor(Type.EmptyTypes)?.Invoke(null) as T);

        }

        public T AddComponent<T>(T component) where T: class, IComponent  {

            if (component == null) {

                return null;

            }

            Type componentType = typeof(T);

            lock(components) {

                if (!components.ContainsKey(componentType)) {

                    components.Add(componentType, component);

                }

            }

            return component;

        }

        public void Dispose() {

            lock(components) {

                components?.Clear();

            }

            IdObject = null;

        }

    }

}
