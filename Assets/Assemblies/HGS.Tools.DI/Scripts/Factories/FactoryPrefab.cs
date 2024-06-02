using UnityEngine;

namespace HGS.Tools.DI.Factories {

    public class FactoryPrefab<T>: Factory<T> where T: Object {

        private readonly Object prefab;

        public FactoryPrefab(Object prefab) {

            this.prefab = prefab;

        }

        public override object Create() {

            return (Object.Instantiate(prefab) as GameObject)?.GetComponent<T>();

        }

    }

}
