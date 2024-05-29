using System;
using HGS.Asteroids.GameObjects;
using HGS.Asteroids.Services;
using HGS.Enums;
using HGS.Tools.DI;
using HGS.Tools.Services.Pools;
using UnityEngine;

namespace HGS.Asteroids.Binders {

    public class SceneGameBinder: Binder {

        [field:SerializeField]
        private GameObject InputValuesPlayerPrefab { get; set; }
        [field:SerializeField]
        private GameObject InputValuesUIPrefab { get; set; }

        [field:SerializeField]
        private GameObject SpaceshipPrefab { get; set; }

        [field:Header("Bullet Prefabs")]
        [field:SerializeField]
        private GameObject BulletGunPrefab { get; set; }
        [field:SerializeField]
        private GameObject BulletUfoPrefab { get; set; }
        [field:SerializeField]
        private GameObject RayLaserPrefab { get; set; }

        [field:Header("Explosion Prefabs")]
        [field:SerializeField]
        private GameObject ExplosionPrefab { get; set; }
        [field:SerializeField]
        private GameObject ExplosionSmallPrefab { get; set; }

        private PoolStack poolStack;

        #region Awake/Start/Update/FixedUpdate

        private new void OnDestroy() {

            base.OnDestroy();

            poolStack?.Destroy();

        }

        #endregion

        public override void Bind() {

            Debug.Log("SceneGameBinder.Bind");
            
            BindInputValues();
            BindSpaceship();
            
            BindPoolStack();

        }

        private void BindPoolStack() {
            
            poolStack = new PoolStack();

            poolStack.RegisterPool(ObjectVariant.BulletGun, new GameObjectPool(BulletGunPrefab));
            poolStack.RegisterPool(ObjectVariant.BulletUfo, new GameObjectPool(BulletUfoPrefab));
            poolStack.RegisterPool(ObjectVariant.RayLaser, new GameObjectPool(RayLaserPrefab));

            poolStack.RegisterPool(ObjectVariant.Explosion, new GameObjectPool(ExplosionPrefab));
            poolStack.RegisterPool(ObjectVariant.ExplosionSmall, new GameObjectPool(ExplosionSmallPrefab));
            
            Container.BindFromInstance<IPoolStack, PoolStack>(poolStack).AsSingle();

        }

        private void BindSpaceship() {
            
            // выриант генерации через SpawnerSpaceship
            Container.BindFromPrefab<Spaceship>(SpaceshipPrefab).AsSingle(false);

        }

        private void BindInputValues() {
            
            Container.BindFromPrefab<InputValuesPlayer>(InputValuesPlayerPrefab).AsSingle();
            Container.BindFromPrefab<InputValuesUI>(InputValuesUIPrefab).AsSingle();

        }
        
    }

}