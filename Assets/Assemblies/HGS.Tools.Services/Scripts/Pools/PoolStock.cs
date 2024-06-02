using System;
using System.Collections.Generic;

namespace HGS.Tools.Services.Pools {

    public class PoolStock {

        private readonly Dictionary<Type, IPool> pools = new ();
        private readonly List<KeyValuePair<WeakReference, Type>> objects = new (); // TODO: более оптимальное решение по определению Type
        
        private readonly object lockObject = new object();

        public object Get(Type type) {
            
            lock(lockObject) {

                if (pools.ContainsKey(type)) {

                    object obj = pools[type]?.Get();

                    if (obj != null) {

                        objects.Add(new (new WeakReference(obj), type));

                    }

                    return obj;

                }

            }

            return null;

        }

        public object Get<T>() {
            
            return Get(typeof(T));

        }

        private void Release(Type type, object releasedObject) {
            
            if (releasedObject == null) {

                return;

            }
            
            lock(lockObject) {

                if (pools.ContainsKey(type)) {

                    pools[type]?.Release(releasedObject);

                }

                // удаляем объект из списка
                for(int i = 0; i < objects.Count; i++) {

                    if (objects[i].Key?.Target == releasedObject) {

                        objects.RemoveAt(i);
                        break;

                    }

                }

            }

        }

        public void Release(object releasedObject) {
            
            if (releasedObject == null) {

                return;

            }

            Type type = null;

            lock(lockObject) {

                // определяем тип по объекту
                for(int i = 0; i < objects.Count; i++) {

                    if (objects[i].Key?.Target == releasedObject) {

                        type = objects[i].Value;
                        break;

                    }

                }

            }

            if (type != null) {
                
                Release(type, releasedObject);

            }

        }

        public void Release<T>(object releasedObject) {
            
            Release(typeof(T), releasedObject);

        }

        public void RegisterPool(Type type, IPool pool) {
            
            lock(lockObject) {

                if (!pools.ContainsKey(type)) {

                    pools.Add(type, pool);

                }

            }

        }

        public void RegisterPool<T>(IPool pool) {
            
            RegisterPool(typeof(T), pool);

        }

        public void Destroy() {

            lock(lockObject) {

                foreach(var pool in pools) {

                    pool.Value?.Clear();

                }

                pools.Clear();

                objects.Clear();

            }

        }

    }

}