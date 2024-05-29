using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public abstract class LimitedSupplyWeapon: Weapon, ILimitedSupplyWeapon {

        [field:Header("LimitedSupplyWeapon")]
        [field:SerializeField]
        public int MaxBulletCount { get; private set; }
        [field:SerializeField]
        public int BulletCount { get; private set; }
        
        public override void Shoot(Vector2 direction) {
            
            base.Shoot(direction);
            BulletCount--;

        }

        public override bool CanShoot() {

            return BulletCount > 0 && base.CanShoot();

        }

        protected virtual void AddBullet() {

            if (BulletCount < MaxBulletCount) {

                BulletCount++;

            }

        }

    }

}