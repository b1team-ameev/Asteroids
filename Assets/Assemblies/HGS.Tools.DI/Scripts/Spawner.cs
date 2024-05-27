using HGS.Tools.DI.Factories;
using HGS.Tools.DI.Injection;

namespace HGS.Tools.DI {

    public class Spawner<T>: InjectedMonoBehaviour {
    
        protected IFactory factory;

        [Inject]
        private void Constructor(IFactory<T> factory) {
            
            this.factory = factory;

        }

        #region Awake/Start/Update/FixedUpdate

        // !!! можно (нужно?) усложнить, отвязав спавн от Start
        protected void Start() {

            Spawn(); 

            Destroy(gameObject);

        }

        #endregion

        // !!! можно (нужно?) усложнить, добавив свой интерфейс
        public virtual T Spawn() {

            return (T)factory?.Create();

        }

    }

}