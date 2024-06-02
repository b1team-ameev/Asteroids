using HGS.Tools.DI.Injection;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.GameObjects.Spawners {

    public abstract class Spawner<T>: InjectedMonoBehaviour, ISpawner {
        
        [field:SerializeField]
        private Transform Container { get; set; }

        [field:SerializeField]
        private bool IsSpawnOnStart { get; set; }
        [field:SerializeField]
        private bool IsUseSpawnPosition { get; set; }

        private EntityStock entityStock;

        [Inject]
        private void Constructor(EntityStock entityStock) {
            
            this.entityStock = entityStock;

        }

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            if (IsSpawnOnStart) {

                Spawn();

            }

        }

        #endregion

        public void Spawn() {

            if (entityStock != null) {

                Transform thisTransform = transform;

                IEntity entity = entityStock.GetEntityFactory<T>()?.Create();
                TransformComponent transformComponent = entity?.GetComponent<TransformComponent>();

                if (transformComponent != null) {

                    transformComponent.Transform.parent = Container != null ? Container : thisTransform.parent;

                    if (IsUseSpawnPosition) {

                        transformComponent.Transform.position = thisTransform.position;

                    }

                }

            }

        }

    }

}