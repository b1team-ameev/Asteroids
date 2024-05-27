using UnityEngine;

namespace HGS.Tools.DI.Injection {

    public abstract class InjectedMonoBehaviour: MonoBehaviour {

        #region Awake/Start/Update/FixedUpdate

        protected void Awake() {

            Injector.Inject(this);

        }

        #endregion

    }

}