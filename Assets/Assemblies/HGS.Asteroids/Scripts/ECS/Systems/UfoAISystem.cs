using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Asteroids.ECS.Systems.ShootingSystems;
using HGS.Asteroids.GameObjects;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class UfoAISystem: ShootingSystem, ISystem {

        private SpaceshipObject spaceshipObject;
        private Transform spaceshipTransform;

        public UfoAISystem(EntityStock entityStock, SpaceshipObject spaceshipObject): base(entityStock) {

            this.spaceshipObject = spaceshipObject;

        }

        [EntityFilter(typeof(EntityFilter<UfoAIComponent, IStandartWeaponComponent, TransformComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            if (spaceshipObject == null || spaceshipObject.GameObject == null) {

                spaceshipTransform = null;

            }
            else if (spaceshipTransform == null) {

                spaceshipTransform = spaceshipObject.GameObject.GetComponent<Transform>();

            }

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    UfoAIComponent ufoAIComponent = entity.Get<UfoAIComponent>(0);
                    
                    IStandartWeaponComponent standartWeaponComponent = entity.Get<IStandartWeaponComponent>(1);
                    TransformComponent transformComponent = entity.Get<TransformComponent>(2);

                    Transform thisTransform = transformComponent?.Transform;

                    if (ufoAIComponent != null && thisTransform != null) {

                        // стрельба
                        if (Random.value < ufoAIComponent.ShootingProbability * Time.deltaTime) {

                            if (standartWeaponComponent != null && CanShoot(standartWeaponComponent)) {

                                if (spaceshipTransform == null || !(Random.value < ufoAIComponent.AimedShootingProbability)) {

                                    Shoot(standartWeaponComponent, thisTransform, thisTransform.rotation * Vector2.up);

                                }
                                else {

                                    // прицельный огонь
                                    Shoot(standartWeaponComponent, thisTransform, spaceshipTransform.position - thisTransform.position);

                                }

                            }

                        }

                        // смена направления
                        if (Random.value < ufoAIComponent.ChangeMoveDirectionProbability * Time.deltaTime) {

                            if (spaceshipTransform != null) {

                                // на корабль
                                Vector2 moveVector = thisTransform.rotation * Vector2.up;
                                Vector2 spaceshipVector = spaceshipTransform.position - thisTransform.position;

                                float angle = Vector2.SignedAngle(moveVector, spaceshipVector);

                                // Debug.Log($"moveVector {moveVector}; spaceshipVector {spaceshipVector}; angle {angle}; ");

                                thisTransform.Rotate(0f, 0f, angle);

                            }
                            else {

                                thisTransform.Rotate(new Vector3(0f, 0f, Random.value * 360f));

                            }

                        }

                    }

                }

            }
            
        }

        public new void Dispose() {

            base.Dispose();

            spaceshipObject = null;

        }

    }

}