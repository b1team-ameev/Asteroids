using System;

namespace HGS.Asteroids.ECS.Components.Weapons {

    public abstract class WeaponComponent: IWeaponComponent {

        public Type BulletType { get; }
        public float Timeout { get; }

        public WeaponComponent(Type bulletType, float timeout) {
            
            BulletType = bulletType;
            Timeout = timeout;

        }

        // промежуточное состояние
        public float LastShootTime { get; set; }

    }

}