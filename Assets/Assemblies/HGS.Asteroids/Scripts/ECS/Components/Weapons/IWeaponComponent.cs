using System;
using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components.Weapons {

    public interface IWeaponComponent: IComponent {

        public Type BulletType { get; }
        public float Timeout { get; }

        public float LastShootTime { get; set; }

    }

}