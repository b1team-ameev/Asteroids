using HGS.Tools.DI.Factories;
using HGS.Tools.DI.Injection;

namespace HGS.Tools.DI {

    public class Spawner<T>: InjectedMonoBehaviour {
    
        protected IFactory factory;

        [Inject]
        private void Constructor(IFactory<T> factory) {
            
            this.factory = factory;

        }

        public virtual T Spawn() {

            return (T)factory?.Create();

        }

    }

}