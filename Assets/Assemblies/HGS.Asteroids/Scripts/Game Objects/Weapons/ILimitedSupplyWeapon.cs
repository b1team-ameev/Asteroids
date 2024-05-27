namespace HGS.Asteroids.GameObjects.Weapons {

    public interface ILimitedSupplyWeapon: IWeapon {
        
        public int MaxBulletCount { get; }
        public int BulletCount { get; }

    }

}