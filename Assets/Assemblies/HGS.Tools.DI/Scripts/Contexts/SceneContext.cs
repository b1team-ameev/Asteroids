using UnityEngine;

namespace HGS.Tools.DI.Contexts {

    public class SceneContext: Context {

        #region Awake/Start/Update/FixedUpdate

        protected new void Awake() {

            CheckProjectContext();

            base.Awake();

            Bind();

        }

        #endregion

        private void CheckProjectContext()  {

            IContext projectContext = GlobalContext.Resolve<IContext>();

            if (projectContext == null) {

                Instantiate(Resources.Load("ProjectContext"));

            }

        }

    }

}
