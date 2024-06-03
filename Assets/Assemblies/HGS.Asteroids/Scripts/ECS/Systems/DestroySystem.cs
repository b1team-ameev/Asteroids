using System;
using HGS.Asteroids.ECS.Components;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class DestroySystem: ISystem, IDisposable {

        [EntityFilter(typeof(EntityFilter<DestroyComponent, GameObjectComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private EntityStock entityStock;

        public DestroySystem(EntityStock entityStock) {

            this.entityStock = entityStock;

        }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    DestroyComponent destroyComponent = entity.Get<DestroyComponent>(0);
                    GameObjectComponent gameObjectComponent = entity.Get<GameObjectComponent>(1);

                    if (destroyComponent != null) {

                        DestroyEntity(gameObjectComponent?.GameObject);

                    }

                }

            }
            
        }

        private void DestroyEntity(GameObject gameObject) {
            
            if (gameObject == null) {

                return;

            }

            entityStock?.RemoveEntity(gameObject);
            UnityEngine.Object.Destroy(gameObject);

        }

        public void Dispose() {
            
            entityStock = null;
            
        }

    }

}