using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Asteroids.ECS.Components;
using UnityEngine;
using HGS.Tools.Services.Pools;

namespace HGS.Asteroids.ECS.EntityFactories {

    public class ExplosionEntityFactory<T>: IEntityFactory {

        public readonly float timeBeforeDestruction;
        
        private EntityStock entityStock;
        private PoolStock poolStock;

        public ExplosionEntityFactory(EntityStock entityStock, PoolStock poolStock, float timeBeforeDestruction) {

            this.poolStock = poolStock;
            this.entityStock = entityStock;

            this.timeBeforeDestruction = timeBeforeDestruction;

        }

        public IEntity Create() {
            
            IEntity entity = entityStock?.NewEntity();

            if (timeBeforeDestruction > 0f) {

                entity?.AddComponent(new ReleaseByTimeComponent(timeBeforeDestruction));

            }

            GameObject gameObject = poolStock?.Get<T>() as GameObject;

            if (gameObject != null) {

                entity?.SetIdObject(gameObject);
                entity?.AddComponent(new GameObjectComponent(gameObject));

                entity?.AddComponent(new TransformComponent(gameObject.transform));

                Animator animator = gameObject?.GetComponent<Animator>();
                if (animator != null) {

                    animator.Play("Normal"); // анимация запускается сначала

                }

            }
            
            return entity;

        }

        public void Destroy() {
            
            poolStock = null;
            entityStock = null;

        }
    }

}