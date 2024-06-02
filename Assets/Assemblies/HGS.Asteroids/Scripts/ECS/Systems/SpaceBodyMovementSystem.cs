using HGS.Asteroids.ECS.Components;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceBodyMovementSystem: ISystem {

        [EntityFilter(typeof(EntityFilter<SpaceBodyMovementComponent, TransformComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    SpaceBodyMovementComponent spaceBodyMovementComponent = entity.Get<SpaceBodyMovementComponent>(0);
                    TransformComponent transformComponent = entity.Get<TransformComponent>(1);

                    if (spaceBodyMovementComponent != null && transformComponent != null) {

                        transformComponent.Transform.Translate(Vector2.up * spaceBodyMovementComponent.Speed * Time.deltaTime);

                    }

                }

            }
            
        }

    }

}