using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public abstract class Weapon: MonoBehaviour, IWeapon {

        [field:SerializeField]
        private GameObject BulletPrefab { get; set; }
        [field:SerializeField]
        private float Timeout { get; set; }

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

            if (BulletPrefab != null && thisTransform != null) {

                Instantiate(BulletPrefab, thisTransform.position + (Vector3)direction.normalized * 0.5f,
                    Quaternion.FromToRotation(Vector2.up, direction), thisTransform != null ? thisTransform.parent : thisTransform);

            }

            lastShootTime = Time.time;

        }

    }

}