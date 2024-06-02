using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems.ShootingSystems {

    public class LimitedSupplyWeaponShootingSystem: ShootingSystem {

        public LimitedSupplyWeaponShootingSystem(EntityStock entityStock): base(entityStock) {

        }

        public override bool CanShoot(IWeaponComponent weapon) {
            
            var limitedSupplyWeapon = weapon as ILimitedSupplyWeaponComponent;

            if (weapon == null || limitedSupplyWeapon == null) {

                return false;

            }

            return limitedSupplyWeapon.BulletCount > 0 && base.CanShoot(weapon);

        }        

        public override void Shoot(IWeaponComponent weapon, Transform transform, Vector2 direction) {
            
            var limitedSupplyWeapon = weapon as ILimitedSupplyWeaponComponent;

            if (weapon == null || limitedSupplyWeapon == null || transform == null) {

                return;

            }

            base.Shoot(weapon, transform, direction);
            limitedSupplyWeapon.BulletCount--;

        }

    }

}