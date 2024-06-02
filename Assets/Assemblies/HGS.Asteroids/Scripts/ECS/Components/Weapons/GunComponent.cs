using System;

namespace HGS.Asteroids.ECS.Components.Weapons {

    public class GunComponent<T>: WeaponComponent, IStandartWeaponComponent {

        public GunComponent(float timeout) : base(typeof(T), timeout) {

        }

    }

}