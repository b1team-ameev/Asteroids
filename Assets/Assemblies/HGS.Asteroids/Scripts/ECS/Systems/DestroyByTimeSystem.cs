using System;
using HGS.Asteroids.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class DestroyByTimeSystem: ISystem, IDisposable {

        [EntityFilter(typeof(EntityFilter<DestroyByTimeComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private EntityStock entityStock;

        public DestroyByTimeSystem(EntityStock entityStock) {

            this.entityStock = entityStock;

        }

        public void Run() {

            bool isNeedRefreshEntityFilters = false;

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    DestroyByTimeComponent destroyByTimeComponent = entity.Get<DestroyByTimeComponent>(0);

                    if (destroyByTimeComponent != null) {

                        if (destroyByTimeComponent.CurrentTime < destroyByTimeComponent.TimeBeforeDestruction) {

                            destroyByTimeComponent.CurrentTime += Time.deltaTime;

                        }
                        else {

                            entity?.Entity?.AddComponent<DestroyComponent>();
                            isNeedRefreshEntityFilters = true;

                        }

                    }

                }

            }

            if (isNeedRefreshEntityFilters) {

                entityStock?.RefreshEntityFilters();

            }
            
        }

        public void Dispose() {
            
            entityStock = null;
            
        }

    }

}