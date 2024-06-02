using HGS.Tools.DI.Injection;
using HGS.Tools.ECS.Systems;

namespace HGS.Asteroids.ECS {

    public class World: InjectedMonoBehaviour {

        private SystemStock systemStock;

        [Inject]
        private void Constructor(SystemStock systemStock) {
            
            this.systemStock = systemStock;

        }

        #region Awake/Start/Update/FixedUpdate

        private void Update() {

            systemStock?.Run();

        }

        #endregion

    }

}