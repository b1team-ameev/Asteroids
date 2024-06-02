using System;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems.ShootingSystems {

    public class ShootingSystem: IDisposable {

        protected EntityStock entityStock;

        public ShootingSystem(EntityStock entityStock) {

            this.entityStock = entityStock;

        }

        public virtual bool CanShoot(IWeaponComponent weapon) {
            
            if (weapon == null) {

                return false;

            }

            return weapon.LastShootTime == default || Time.time - weapon.LastShootTime > weapon.Timeout;

        }        

        public virtual void Shoot(IWeaponComponent weapon, Transform transform, Vector2 direction) {
            
            if (weapon == null || transform == null) {

                return;

            }

            IEntityFactory entityFactory = entityStock?.GetEntityFactory(weapon.BulletType);
            IEntity entity = entityFactory?.Create();

            if (entity != null) {

                Transform bulletTransform = entity.GetComponent<TransformComponent>()?.Transform;

                if (bulletTransform != null) {

                    bulletTransform.parent = transform.parent;

                    bulletTransform.position = transform.position + (Vector3)direction.normalized * 0.5f;
                    bulletTransform.rotation = Quaternion.FromToRotation(Vector2.up, direction);

                }

            }
            
            weapon.LastShootTime = Time.time;

        }

        public void Dispose() {
            
            entityStock = null;

        }

    }

}