using System;

namespace HGS.Asteroids.ECS.Components.Weapons {

    public abstract class ReloadableByTimeWeaponComponent: LimitedSupplyWeaponComponent, IReloadableWeaponComponent {

        public float ReloadTime { get; }
        public float TimeBeforeReload { get; set; }

        protected ReloadableByTimeWeaponComponent(Type bulletType, float timeout, int maxBulletCount, float reloadTime): 
            base(bulletType, timeout, maxBulletCount) {

            ReloadTime = reloadTime;
            TimeBeforeReload = reloadTime;

        }

        // промежуточное состояние
        public float CurrentReloadTime { get; set; }

    }

}