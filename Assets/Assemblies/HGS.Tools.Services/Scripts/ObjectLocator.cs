using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HGS.Tools.Services.ServiceObjectLocator {

    public class ObjectLocator : MonoBehaviour {

        [field:SerializeField]
        private GameObject[] Objects { get; set; }

        private Dictionary<string, Component> arComponents = new Dictionary<string, Component>();

        public T GetObject<T>(string name) where T : Component {

            string key = typeof(T).ToString() + "_" + name;

            if (arComponents.ContainsKey(key)) {

                return (T)arComponents[key];

            }

            foreach(GameObject obj in Objects) {

                if (obj != null && obj.name == name) {

                    T result = obj.GetComponent<T>();

                    if (!arComponents.ContainsKey(key)) {

                        arComponents.Add(key, result);

                    }

                    return result;

                }

            }

            return null;

        }

        public T GetObject<T>() where T : Component {

            return GetObject<T>(typeof(T).ToString());

        }

        public Transform GetObject(string name) {

            return GetObject<Transform>(name);

        }

        public T Instantiate<T>(T prefab, string container) where T : Component {

            return Instantiate(prefab, Vector3.zero, Quaternion.identity, GetObject(container));

        }
        
    }

}