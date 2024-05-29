using HGS.Enums;
using HGS.Tools.DI.Injection;
using HGS.Tools.Services.Pools;
using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public abstract class Weapon: InjectedMonoBehaviour, IWeapon {

        [field:SerializeField]
        private ObjectVariant BulletVariant { get; set; }
        [field:SerializeField]
        private float Timeout { get; set; }

        private IPoolStack poolStack;

        [Inject]
        private void Constructor(IPoolStack poolStack) {
            
            this.poolStack = poolStack;

        }

        protected float lastShootTime;

        public virtual void Shoot(Vector2 direction) {
            
            if (CanShoot()) {

                SpawnBullet(direction);

            }

        }

        protected virtual bool CanShoot() {

            return lastShootTime == default || Time.time - lastShootTime > Timeout;

        }

        protected virtual void SpawnBullet(Vector2 direction) {

            Transform thisTransform = transform;

            GameObject bullet = poolStack?.Get(BulletVariant) as GameObject;

            if (bullet != null && thisTransform != null) {

                Transform bulletTransform = bullet.transform;

                if (bulletTransform != null) {

                    bulletTransform.parent = thisTransform.parent;

                    bulletTransform.position = thisTransform.position + (Vector3)direction.normalized * 0.5f;
                    bulletTransform.rotation = Quaternion.FromToRotation(Vector2.up, direction);

                }

            }

            lastShootTime = Time.time;

        }

    }

}