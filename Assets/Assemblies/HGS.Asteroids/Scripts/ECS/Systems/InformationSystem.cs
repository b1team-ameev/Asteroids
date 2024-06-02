using System;
using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Enums;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class InformationSystem: ISystem, IDisposable {

        private const float DELTA_TIME_LIMIT = 0.1f;

        [EntityFilter(typeof(EntityFilter<IReloadableWeaponComponent, TransformInfoComponent, SpaceshipMovementComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        private Events events;

        public InformationSystem(Events events) {
            
            this.events = events;

        }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    IReloadableWeaponComponent reloadableWeaponComponent = entity.Get<IReloadableWeaponComponent>(0);
                    TransformInfoComponent transformComponent = entity.Get<TransformInfoComponent>(1);
                    SpaceshipMovementComponent spaceshipMovementComponent = entity.Get<SpaceshipMovementComponent>(2);

                    if (reloadableWeaponComponent != null) {

                        ShowBulletCount(reloadableWeaponComponent.BulletCount);
                        ShowTimeBeforeReload(reloadableWeaponComponent.TimeBeforeReload);

                    }

                    if (transformComponent != null) {

                        ShowTransform(transformComponent, spaceshipMovementComponent);

                    }

                }

            }
            
        }

        private void ShowBulletCount(int value = default) {

            events?.Raise(EventKey.OnShowBulletCount, new UniversalEventArgs<int>(value));

        }

        private void ShowTimeBeforeReload(float value = default) {

            events?.Raise(EventKey.OnShowTimeBeforeReload, new UniversalEventArgs<float>(value));

        }

        private void ShowTransform(TransformInfoComponent transformComponent, SpaceshipMovementComponent spaceshipMovementComponent) {
            
            Transform transform = transformComponent?.Transform;

            if (transform != null) {

                ShowTransform(transform.position.x, transform.position.y, transformComponent.speed, transform.rotation.eulerAngles.z);
                
                // оптимизация вычисления скорости
                transformComponent.time += Time.deltaTime;

                if (transformComponent.time >= DELTA_TIME_LIMIT) {

                    transformComponent.speed = (transformComponent.prevPosition - (Vector2)transform.position).magnitude / transformComponent.time;
                    transformComponent.time = 0f;

                    if (spaceshipMovementComponent != null) {

                        transformComponent.speed = Mathf.Min(transformComponent.speed, spaceshipMovementComponent.FlightPower);

                    }

                    transformComponent.prevPosition = transform.position; 

                }

            }

        }

        private void ShowTransform(float x = default, float y = default, float speed = default, float angle = default) {

            events?.Raise(EventKey.OnShowCoordinates, string.Format("{0}; {1}", x.ToString("N1"), y.ToString("N1")));

            events?.Raise(EventKey.OnShowInstantSpeed, string.Format("{0,4:#0.0} units/s", speed));

            events?.Raise(EventKey.OnShowRotationAngle, string.Format("{0,5:##0.0}°", angle));

        }

        public void Dispose() {
            
            events = null;
            
        }

    }

}