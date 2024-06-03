using HGS.Tools.DI.Injection;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS {

    public class World: InjectedMonoBehaviour {

        [field:SerializeField]
        public SystemStock SystemStock { get; private set; }
        [field:SerializeField]
        public EntityStock EntityStock { get; private set; }

        [Inject]
        private void Constructor(SystemStock systemStock, EntityStock entityStock) {
            
            SystemStock = systemStock;
            EntityStock = entityStock;

        }

        #region Awake/Start/Update/FixedUpdate

        private void Update() {

            SystemStock?.Run();

        }

        #endregion

    }

}