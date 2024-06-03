using System;
using HGS.Asteroids.ECS.Components;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using HGS.Tools.Services.Pools;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class ReleaseByTimeSystem: ISystem, IDisposable {

        [EntityFilter(typeof(EntityFilter<ReleaseByTimeComponent, GameObjectComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private EntityStock entityStock;
        private PoolStock poolStock;

        public ReleaseByTimeSystem(PoolStock poolStock, EntityStock entityStock) {

            this.entityStock = entityStock;
            this.poolStock = poolStock;

        }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    ReleaseByTimeComponent releaseByTimeComponent = entity.Get<ReleaseByTimeComponent>(0);
                    GameObjectComponent gameObjectComponent = entity.Get<GameObjectComponent>(1);

                    if (releaseByTimeComponent != null) {

                        if (releaseByTimeComponent.CurrentTime < releaseByTimeComponent.TimeBeforeReleasing) {

                            releaseByTimeComponent.CurrentTime += Time.deltaTime;

                        }
                        else {

                            ReleaseEntity(gameObjectComponent?.GameObject);

                        }

                    }

                }

            }
            
        }

        private void ReleaseEntity(GameObject gameObject) {
            
            if (gameObject == null) {

                return;

            }

            entityStock?.RemoveEntity(gameObject);
            poolStock?.Release(gameObject);

        }

        public void Dispose() {
            
            entityStock = null;
            poolStock = null;
            
        }

    }

}