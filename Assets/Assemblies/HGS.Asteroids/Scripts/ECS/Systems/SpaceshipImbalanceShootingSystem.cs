using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Asteroids.ECS.Systems.ShootingSystems;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceshipImbalanceShootingSystem: ReloadableWeaponShootingSystem, ISystem {

        public SpaceshipImbalanceShootingSystem(EntityStock entityStock): base(entityStock) {

        }

        [EntityFilter(typeof(EntityFilter<SpaceshipComponent, IImbalanceWeaponComponent, InputValuesPlayerComponent, TransformComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    IImbalanceWeaponComponent iImbalanceWeaponComponent = entity.Get<IImbalanceWeaponComponent>(1);

                    InputValuesPlayerComponent inputValuesPlayerComponent = entity.Get<InputValuesPlayerComponent>(2); 
                    TransformComponent transformComponent = entity.Get<TransformComponent>(3);

                    if (inputValuesPlayerComponent != null) {

                        bool isAltFired = inputValuesPlayerComponent != null ? inputValuesPlayerComponent.InputValuesPlayer.IsAltFire : false;

                        if (isAltFired && CanShoot(iImbalanceWeaponComponent)) {

                            Transform baseTransform = entity.Entity?.GetComponent<TransformBaseComponent>()?.Transform;
                            Vector2 direction = baseTransform != null ? baseTransform.rotation * Vector2.up : Vector2.up;

                            Shoot(iImbalanceWeaponComponent, transformComponent?.Transform, direction);
                            
                        }

                        Reload(iImbalanceWeaponComponent);

                    }

                }

            }
            
        }

    }

}