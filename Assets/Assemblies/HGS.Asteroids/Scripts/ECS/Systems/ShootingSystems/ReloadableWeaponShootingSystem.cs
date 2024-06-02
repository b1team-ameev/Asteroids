using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems.ShootingSystems {

    public class ReloadableWeaponShootingSystem: LimitedSupplyWeaponShootingSystem {

        public ReloadableWeaponShootingSystem(EntityStock entityStock): base(entityStock) {

        }

        public void Reload(IWeaponComponent reloadableWeapon) {
            
            var weapon = reloadableWeapon as IReloadableWeaponComponent;

            if (weapon == null) {

                return;

            }

            if (weapon.BulletCount < weapon.MaxBulletCount) {

                if (weapon.CurrentReloadTime < weapon.ReloadTime) {

                    weapon.CurrentReloadTime += Time.deltaTime;
                    weapon.TimeBeforeReload = weapon.ReloadTime - weapon.CurrentReloadTime;

                }
                else {

                    weapon.CurrentReloadTime = 0f;
                    weapon.TimeBeforeReload = 0f;

                    AddBullet(weapon);

                }

            }
            else {

                weapon.TimeBeforeReload = weapon.ReloadTime;

            }

        }

        private void AddBullet(ILimitedSupplyWeaponComponent weapon) {

            if (weapon == null) {

                return;

            }

            if (weapon.BulletCount < weapon.MaxBulletCount) {

                weapon.BulletCount++;

            }

        }

    }

}