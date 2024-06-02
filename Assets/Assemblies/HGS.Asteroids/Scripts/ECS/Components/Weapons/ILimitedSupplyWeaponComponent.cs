namespace HGS.Asteroids.ECS.Components.Weapons {

    public interface ILimitedSupplyWeaponComponent: IWeaponComponent {
        
        public int MaxBulletCount { get; }
        public int BulletCount { get; set; }

    }

}