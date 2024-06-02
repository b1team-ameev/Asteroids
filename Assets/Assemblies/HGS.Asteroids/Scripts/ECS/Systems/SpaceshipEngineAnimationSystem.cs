using HGS.Asteroids.ECS.Components;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceshipEngineAnimationSystem: ISystem {

        [EntityFilter(typeof(EntityFilter<SpaceshipMovementComponent, SpaceshipEngineAnimatorComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    SpaceshipMovementComponent spaceshipMovementComponent = entity.Get<SpaceshipMovementComponent>(0);
                    SpaceshipEngineAnimatorComponent spaceshipEngineAnimatorComponent = entity.Get<SpaceshipEngineAnimatorComponent>(1); 

                    if (spaceshipMovementComponent != null && spaceshipEngineAnimatorComponent != null) {

                        if (spaceshipEngineAnimatorComponent.IsFlighting != spaceshipMovementComponent.IsFlighting) {

                            spaceshipEngineAnimatorComponent.IsFlighting = spaceshipMovementComponent.IsFlighting;
                            spaceshipEngineAnimatorComponent.Animator.SetFloat("IsFlighting", spaceshipEngineAnimatorComponent.IsFlighting ? 1f : 0f);

                        }

                    }

                }

            }
            
        }

    }

}