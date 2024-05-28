using System;
using System.Collections.Generic;
using System.Linq;
using HGS.Tools.DI.Contexts;
using HGS.Tools.DI.Factories;

namespace HGS.Tools.DI {

    public class DIContainer {
        
        private readonly Dictionary<Type, IFactory> binds = new Dictionary<Type, IFactory>();
        
        private IContext context;

        public DIContainer(IContext context) {

            this.context = context;

        }

        public void Destroy() {
            
            lock(binds) {

                binds?.Clear();

            }

            context = null;

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

        public DIContainerWrapper<TInterface, TType> BindAsSingle<TInterface, TType>(bool isCreate = true) where TType: TInterface {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (binds.ContainsKey(type)) {

                    IFactory<TType> factory = new FactorySingletone<TType>(binds[type] as IFactory<TType>);

                    binds[type] = factory;

                    if (isCreate) {

                        factory.Create();

                    }

                    context?.OnBind<TInterface>();

                }

            }

            return new DIContainerWrapper<TInterface, TType>(this);

        }         

        public DIContainerWrapper<TType, TType> BindAsSingle<TType>(bool isCreate = true) {
            
            return BindAsSingle<TType, TType>(isCreate);

        }   

        public DIContainerWrapper<TInterface, TType> BindTo<TInterface, TType>() where TType: TInterface, new() {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryConstructor<TType>());

                }

            }

            return new DIContainerWrapper<TInterface, TType>(this);

        }    

        public DIContainerWrapper<TInterface, TType> BindFromInstance<TInterface, TType>(TType instance) where TType: TInterface {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryInstance<TType>(instance));

                }

            }

            return new DIContainerWrapper<TInterface, TType>(this);

        }    

        public DIContainerWrapper<TType, TType> BindFromInstance<TType>(TType instance) {
            
            return BindFromInstance<TType, TType>(instance);

        } 

        public DIContainerWrapper<TInterface, TType> BindFromPrefab<TInterface, TType>(UnityEngine.Object prefab) where TType: UnityEngine.Object, TInterface {
            
            lock(binds) {

                Type type = typeof(TInterface);

                if (!binds.ContainsKey(type)) {

                    binds.Add(type, new FactoryPrefab<TType>(prefab));

                }

            }

            return new DIContainerWrapper<TInterface, TType>(this);

        }

        public DIContainerWrapper<TType, TType> BindFromPrefab<TType>(UnityEngine.Object prefab) where TType: UnityEngine.Object {
            
            return BindFromPrefab<TType, TType>(prefab);

        }

        public void BindIsOver<TInterface>() {

            context?.OnBind<TInterface>();

        }

    }

}
