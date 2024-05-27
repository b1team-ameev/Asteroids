using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public abstract class ReloadableByTimeWeapon: LimitedSupplyWeapon, IReloadableWeapon {

        [field:Header("ReloadableWeapon")]
        [field:SerializeField]
        public float ReloadTime { get; private set; }
        [field:SerializeField]
        public float TimeBeforeReload { get; private set; }

        private float currentReloadTime;

        #region Awake/Start/Update/FixedUpdate

        private void Update() {

            Reload();

        }

        #endregion

        private void Reload() {
            
            if (BulletCount < MaxBulletCount) {

                if (currentReloadTime < ReloadTime) {

                    currentReloadTime += Time.deltaTime;
                    TimeBeforeReload = ReloadTime - currentReloadTime;

                }
                else {

                    currentReloadTime = 0f;
                    TimeBeforeReload = 0f;

                    AddBullet();

                }

            }
            else {

                TimeBeforeReload = ReloadTime;

            }

        }

    }

}