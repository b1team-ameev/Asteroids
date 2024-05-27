using HGS.Tools.DI.Contexts;
using UnityEngine;

namespace HGS.Tools.DI {

    public abstract class Binder: MonoBehaviour {
        
        protected DIContainer Container;

        #region Awake/Start/Update/FixedUpdate

        protected void OnDestroy() {

            Container = null;

        }

        #endregion
        
        public void SetContainer(DIContainer container) {

            Container = container;

        }

        public abstract void Bind();
        
    }

}
