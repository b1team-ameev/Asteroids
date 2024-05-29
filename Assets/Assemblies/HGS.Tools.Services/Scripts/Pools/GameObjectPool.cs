using UnityEngine;
using UnityEngine.Pool;

namespace HGS.Tools.Services.Pools {

    public class GameObjectPool: IPool {

        private readonly ObjectPool<GameObject> pool;
        private readonly GameObject prefab;

        public GameObjectPool(GameObject prefab) {

            this.prefab = prefab;
            pool = new ObjectPool<GameObject>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject);

        }

        private void OnDestroyObject(GameObject gameObject) {
            
            if (gameObject != null) {

                Object.Destroy(gameObject);

            }

        }

        private void OnReleaseObject(GameObject gameObject) {
            
            gameObject?.SetActive(false);

        }

        private void OnGetObject(GameObject gameObject) {
            
            gameObject?.SetActive(true);

        }

        private GameObject OnCreateObject() {
        
            return Object.Instantiate(prefab);

        }

        public object Get() {
            
            return pool?.Get();

        }

        public void Release(object releasedObject) {
            
            pool?.Release((GameObject)releasedObject);

        }

        public void Clear() {
            
            pool?.Clear();

        }

    }

}