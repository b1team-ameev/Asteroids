namespace HGS.Asteroids.ECS.Components.Weapons {

    public class LaserComponent<T>: ReloadableByTimeWeaponComponent, IImbalanceWeaponComponent {

        public LaserComponent(float timeout, int maxBulletCount, float reloadTime) : base(typeof(T), timeout, maxBulletCount, reloadTime) {

        }

    }

}