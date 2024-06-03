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

    public class SpaceBodyCollisionSystem: ISystem, IDisposable {

        private const float ASTEROID_POSITION_DELTA = 0.25f;

        [EntityFilter(typeof(EntityFilter<IAsteroidComponent, TriggerEnterComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private EntityStock entityStock;
        
        private Events events;
        private Sounds sounds;

        public SpaceBodyCollisionSystem(EntityStock entityStock, Events events, Sounds sounds) {
            
            this.entityStock = entityStock;

            this.events = events;
            this.sounds = sounds;

        }
        
        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    IAsteroidComponent asteroidComponent = entity.Get<IAsteroidComponent>(0);
                    TriggerEnterComponent triggerEnterComponent = entity.Get<TriggerEnterComponent>(1);

                    if (asteroidComponent != null) {

                        IDamagerComponent damager = triggerEnterComponent?.OtherEntity?.GetComponent<IDamagerComponent>();

                        if (damager != null) {
               
                            // генерация взрыва
                            Transform thisTransform = entity?.Entity?.GetComponent<TransformComponent>()?.Transform;

                            IEntity explosionEntity = 
                                !(asteroidComponent is AsteroidSmallComponent) ?
                                entityStock?.GetEntityFactory<Explosion>()?.Create() :
                                entityStock?.GetEntityFactory<ExplosionSmall>()?.Create();
                            SetTransform(thisTransform, explosionEntity?.GetComponent<TransformComponent>()?.Transform);

                            // звук взрыва
                            SoundCaseComponent soundCaseComponent = entity?.Entity?.GetComponent<SoundCaseComponent>();
                            if (soundCaseComponent != null) {

                                sounds?.Play(soundCaseComponent.Sound);

                            }
                            
                            // сохраняем очки
                            IPointsComponent pointsComponent = entity?.Entity?.GetComponent<IPointsComponent>();
                            if (pointsComponent != null) {

                                events?.Raise(EventKey.OnAsteroidDestroyed, new UniversalEventArgs<int>(pointsComponent.Value));

                            }

                            // создаем осколки
                            if (damager.Damage != Enums.Damage.Fatal) {

                                if (asteroidComponent is AsteroidBigComponent) {

                                    IEntity shardEntity = entityStock?.GetEntityFactory<AsteroidMiddle>()?.Create();
                                    SetTransform(thisTransform, shardEntity?.GetComponent<TransformComponent>()?.Transform, ASTEROID_POSITION_DELTA);

                                    shardEntity = entityStock?.GetEntityFactory<AsteroidSmall>()?.Create();
                                    SetTransform(thisTransform, shardEntity?.GetComponent<TransformComponent>()?.Transform, ASTEROID_POSITION_DELTA);

                                }
                                else if (asteroidComponent is AsteroidMiddleComponent) {

                                    IEntity shardEntity = entityStock?.GetEntityFactory<AsteroidSmall>()?.Create();
                                    SetTransform(thisTransform, shardEntity?.GetComponent<TransformComponent>()?.Transform, ASTEROID_POSITION_DELTA);

                                    shardEntity = entityStock?.GetEntityFactory<AsteroidSmall>()?.Create();
                                    SetTransform(thisTransform, shardEntity?.GetComponent<TransformComponent>()?.Transform, ASTEROID_POSITION_DELTA);

                                }

                            }

                            // удаление объекта
                            entity?.Entity?.AddComponent<DestroyComponent>();
                            entityStock?.OnEntityUpdate(entity?.Entity);

                        }

                    }

                }

            }
            
        }

        private void SetTransform(Transform mainTransform, Transform newTrasnform, float delta = 0f) {

            if (mainTransform ==  null || newTrasnform == null) {

                return;

            }

            newTrasnform.parent = mainTransform.parent;
            newTrasnform.position = mainTransform.position;

            if (delta > 0f) {

                newTrasnform.position += new Vector3(UnityEngine.Random.Range(-delta, delta), UnityEngine.Random.Range(-delta, delta), 0f); 

            }

        }

        public void Dispose() {
            
            entityStock = null;
            
        }

    }

}