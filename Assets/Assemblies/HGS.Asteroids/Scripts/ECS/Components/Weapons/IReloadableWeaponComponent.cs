namespace HGS.Asteroids.ECS.Components.Weapons {

    public interface IReloadableWeaponComponent: ILimitedSupplyWeaponComponent {

        public float ReloadTime { get; }
        public float TimeBeforeReload { get; set; }

        public float CurrentReloadTime { get; set; }

    }

}