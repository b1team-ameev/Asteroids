namespace HGS.Asteroids.GameObjects.Weapons {

    public interface IReloadableWeapon: IWeapon {

        public float ReloadTime { get; }
        public float TimeBeforeReload { get; }

    }

}