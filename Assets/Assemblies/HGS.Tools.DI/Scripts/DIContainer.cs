using System;
using System.Collections.Generic;
using System.Linq;
using HGS.Tools.DI.Factories;

namespace HGS.Tools.DI.Contexts {

    public class DIContainer {
        
        private readonly Dictionary<Type, IFactory> binds = new Dictionary<Type, IFactory>();

        public void Destroy() {
            
            lock(binds) {

                binds?.Clear();

            }

        }   

        public T Resolve<T>() where T: class {
            
            lock(binds) {

                // предоставление фабрики
                if (typeof(T).GetInterfaces().Contains(typeof(IFactory))) {

                    Type[] types = typeof(T).GetGenericArguments();
                    
                    if (types != null && types.Length > 0) {

                        Type type = types[0];

                        if (binds.ContainsKey(type)) {

                            return binds[type] as T;

                        }

                    }

                }
                else {

                    Type type = typeof(T);

                    if (binds.ContainsKey(type)) {

                        return binds[type].Create() as T;

                    }

                }

            }

            return default;

        }    

        public void BindAsSingle<TInterface, TType>(bool isCreate = true) where TType: TInterface {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (binds.ContainsKey(type)) {

                    IFactory<TType> factory = new FactorySingletone<TType>(binds[type] as IFactory<TType>);

                    binds[type] = factory;

                    if (isCreate) {

                        factory.Create();

                    }

                }

            }

        }         

        public void BindAsSingle<TType>(bool isCreate = true) {
            
            BindAsSingle<TType, TType>(isCreate);

        }   

        public void BindTo<TInterface, TType>() where TType: TInterface, new() {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryConstructor<TType>());

                }

            }

        }    

        public void BindFromInstance<TInterface, TType>(TType instance) where TType: TInterface {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryInstance<TType>(instance));

                }

            }

        } 

        public void BindFromPrefab<TInterface, TType>(UnityEngine.Object prefab) where TType: UnityEngine.Object {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryPrefab<TType>(prefab));

                }

            }

        }

        public void BindFromPrefab<TType>(UnityEngine.Object prefab) where TType: UnityEngine.Object {
            
            BindFromPrefab<TType, TType>(prefab);

        }

    }

}
