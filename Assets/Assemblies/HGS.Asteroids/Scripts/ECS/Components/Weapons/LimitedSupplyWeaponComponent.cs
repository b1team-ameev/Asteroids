using System;

namespace HGS.Asteroids.ECS.Components.Weapons {

    public abstract class LimitedSupplyWeaponComponent: WeaponComponent, ILimitedSupplyWeaponComponent {
        
        public int MaxBulletCount { get; }
        public int BulletCount { get; set; }

        public LimitedSupplyWeaponComponent(Type bulletType, float timeout, int maxBulletCount): base(bulletType, timeout) {
            
            MaxBulletCount = maxBulletCount;
            BulletCount = maxBulletCount;

        }

    }

}