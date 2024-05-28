using UnityEngine;

namespace HGS.Tools.DI.Contexts {

    public abstract class Context: MonoBehaviour, IContext {
        
        [field:SerializeField]
        protected Binder[] Binders { get; set; }

        protected DIContainer Container { get; set; }

        #region Awake/Start/Update/FixedUpdate

        protected void Awake() {

            Container = new DIContainer(this);

            GlobalContext.AddContext(this);

        }

        protected void OnDestroy() {

            GlobalContext.RemoveContext(this);

            Container?.Destroy();
            Container = null;

        }

        #endregion

        public T Resolve<T>() where T: class {
            
            return Container?.Resolve<T>();

        }

        protected void Bind() {

            if (Binders != null) {

                foreach(Binder binder in Binders) {

                    if (binder != null) {

                        binder.SetContainer(Container);
                        binder.Bind();

                    }

                }

            }

        }

        public void OnBind<T>() {

            GlobalContext.OnBind<T>();

        }

    }

}
