using System;
using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Damagers;
using HGS.Asteroids.ECS.Entities;
using HGS.Enums;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceshipCollisionSystem: ISystem, IDisposable {

        [EntityFilter(typeof(EntityFilter<SpaceshipComponent, TriggerEnterComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private EntityStock entityStock;
        
        private Events events;
        private Sounds sounds;

        public SpaceshipCollisionSystem(EntityStock entityStock, Events events, Sounds sounds) {
            
            this.entityStock = entityStock;

            this.events = events;
            this.sounds = sounds;

        }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    SpaceshipComponent spaceshipComponent = entity.Get<SpaceshipComponent>(0);
                    TriggerEnterComponent triggerEnterComponent = entity.Get<TriggerEnterComponent>(1);

                    if (spaceshipComponent != null) {

                        IDamagerComponent damager = triggerEnterComponent?.OtherEntity?.GetComponent<IDamagerComponent>();

                        if (damager != null) {
                            
                            // генерация взрыва
                            Transform thisTransform = entity?.Entity?.GetComponent<TransformComponent>()?.Transform;

                            IEntity explosionEntity = entityStock?.GetEntityFactory<Explosion>()?.Create();
                            Transform explosionTransform = explosionEntity?.GetComponent<TransformComponent>()?.Transform;

                            if (thisTransform != null && explosionTransform != null) {

                                explosionTransform.parent = thisTransform.parent;
                                explosionTransform.position = thisTransform.position;

                            }

                            // звук взрыва
                            sounds?.Play(SoundCase.SpaceshipDestroyed);

                            // уведомление игры
                            events?.Raise(EventKey.OnSpaceshipDestroyed);
                            
                            // удаление объекта
                            entity?.Entity?.AddComponent<DestroyComponent>();                            
                            entityStock?.OnEntityUpdate(entity?.Entity);

                        }

                    }

                }

            }
            
        }

        public void Dispose() {
            
            entityStock = null;
            
        }

    }

}