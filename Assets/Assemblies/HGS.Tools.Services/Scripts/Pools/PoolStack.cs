using System;
using System.Collections.Generic;
using HGS.Enums;

namespace HGS.Tools.Services.Pools {

    public class PoolStack: IPoolStack {

        private readonly Dictionary<ObjectVariant, IPool> pools = new ();
        private readonly List<KeyValuePair<WeakReference, ObjectVariant>> objects = new (); // TODO: более оптимальное решение по определению ObjectVariant
        
        private readonly object lockObject = new object();

        public object Get(ObjectVariant objectVariant) {
            
            lock(lockObject) {

                if (pools.ContainsKey(objectVariant)) {

                    object obj = pools[objectVariant]?.Get();

                    if (obj != null) {

                        objects.Add(new (new WeakReference(obj), objectVariant));

                    }

                    return obj;

                }

            }

            return null;

        }

        private void Release(ObjectVariant objectVariant, object releasedObject) {
            
            lock(lockObject) {

                if (pools.ContainsKey(objectVariant)) {

                    pools[objectVariant]?.Release(releasedObject);

                }

            }

        }

        public void Release(object releasedObject) {
            
            ObjectVariant objectVariant = ObjectVariant.Unknown;

            lock(lockObject) {

                for(int i = 0; i < objects.Count; i++) {

                    if (objects[i].Key?.Target == releasedObject) {

                        objectVariant = objects[i].Value;
                        objects.RemoveAt(i);

                        break;

                    }

                }

            }

            if (objectVariant != ObjectVariant.Unknown) {

                Release(objectVariant, releasedObject);

            }

        }

        public void RegisterPool(ObjectVariant objectVariant, IPool pool) {
            
            lock(lockObject) {

                if (!pools.ContainsKey(objectVariant)) {

                    pools.Add(objectVariant, pool);

                }

            }

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