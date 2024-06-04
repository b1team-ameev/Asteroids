using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Damagers;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;

namespace HGS.Asteroids.ECS.Systems {

    public class BulletCollisionSystem: ISystem {

        [EntityFilter(typeof(EntityFilter<BulletComponent, TriggerEnterComponent>))]
        public IEntityFilter EntityFilter { get; private set; }
       
        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    BulletComponent bulletComponent = entity.Get<BulletComponent>(0);
                    TriggerEnterComponent triggerEnterComponent = entity.Get<TriggerEnterComponent>(1);

                    if (bulletComponent != null) {

                        IDamagerComponent damager = triggerEnterComponent?.OtherEntity?.GetComponent<IDamagerComponent>();

                        if (damager != null) {
               
                            // запрос на возврат в пул
                            ReleaseByTimeComponent releaseByTimeComponent = entity?.Entity?.GetComponent<ReleaseByTimeComponent>();

                            if (releaseByTimeComponent != null) {

                                releaseByTimeComponent.CurrentTime = releaseByTimeComponent.TimeBeforeReleasing;

                            }

                        }

                    }

                }

            }
            
        }

    }

}