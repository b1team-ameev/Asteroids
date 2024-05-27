using System;
using System.Collections.Generic;
using UnityEngine;

namespace HGS.Tools.Components {

    public class MonoBehaviourWithComponentCache: MonoBehaviour {
        
        private Dictionary<Type, object> cachedComponents = new();

        #region Awake/Start/Update/FixedUpdate

        private void OnDestroy() {
            
            cachedComponents?.Clear();

        }

        #endregion

        public new T GetComponent<T>() {

            if (cachedComponents.ContainsKey(typeof(T))) {

                return (T)cachedComponents[typeof(T)];

            }

            var component = base.GetComponent<T>();

            if(component != null) {

                cachedComponents.Add(typeof(T), component);

            }

            return component;

        }

    }

}