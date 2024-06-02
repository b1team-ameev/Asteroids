using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Entities;
using HGS.Asteroids.ECS.EntityFactories;
using HGS.Asteroids.ECS.Systems;
using HGS.Asteroids.Enums;
using HGS.Asteroids.GameObjects;
using HGS.Asteroids.Services;
using HGS.Asteroids.Settings;
using HGS.Enums;
using HGS.Tools.DI;
using HGS.Tools.DI.Factories;
using HGS.Tools.DI.Injection;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.Systems;
using HGS.Tools.Services.Pools;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;
using UnityEngine;

namespace HGS.Asteroids.Binders {

    public class SceneGameBinder: Binder {

        [field:SerializeField]
        private GameObject InputValuesPlayerPrefab { get; set; }
        [field:SerializeField]
        private GameObject InputValuesUIPrefab { get; set; }

        [field:Header("Entity Prefabs")]
        [field:SerializeField]
        private GameObject SpaceshipPrefab { get; set; }
        [field:SerializeField]
        private GameObject AsteroidBigPrefab { get; set; }
        [field:SerializeField]
        private GameObject AsteroidMiddlePrefab { get; set; }
        [field:SerializeField]
        private GameObject AsteroidSmallPrefab { get; set; }
        [field:SerializeField]
        private GameObject UfoPrefab { get; set; }

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

        [field:Header("Settings")]
        [field:SerializeField]
        private SpaceshipMovementSettings SpaceshipMovementSettings { get; set; }
        [field:SerializeField]
        private SpaceshipWeaponSettings SpaceshipWeaponSettings { get; set; }
        [field:SerializeField]
        private UfoSettings UfoSettings { get; set; }

        private PoolStock poolStock;

        private EntityStock entityStock;
        private SystemStock systemStock;

        private Events events;
        private Sounds sounds;

        [Inject]
        private void Constructor(Events events, Sounds sounds) {

            this.events = events;
            this.sounds = sounds;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void OnDestroy() {

            base.OnDestroy();

            poolStock?.Destroy();

            entityStock?.Destroy();
            systemStock?.Destroy();

        }

        #endregion

        public override void Bind() {

            Debug.Log("SceneGameBinder.Bind");
            
            BindInputValues();
            
            BindSpaceship();

            BindPoolStock();
            
            BindWorld();

        }

        private void BindInputValues() {
            
            Container.BindFromPrefab<InputValuesPlayer>(InputValuesPlayerPrefab).AsSingle();
            Container.BindFromPrefab<InputValuesUI>(InputValuesUIPrefab).AsSingle();

        }

        private void BindSpaceship() {
            
            Container.BindFromInstance(new SpaceshipObject(SpaceshipPrefab)).AsSingle(false);

        }

        private void BindPoolStock() {
            
            poolStock = new PoolStock();

            poolStock.RegisterPool<BulletGun>(new GameObjectPool(BulletGunPrefab));
            poolStock.RegisterPool<BulletUfo>(new GameObjectPool(BulletUfoPrefab));
            poolStock.RegisterPool<RayLaser>(new GameObjectPool(RayLaserPrefab));

            poolStock.RegisterPool<Explosion>(new GameObjectPool(ExplosionPrefab));
            poolStock.RegisterPool<ExplosionSmall>(new GameObjectPool(ExplosionSmallPrefab));
            
            Container.BindFromInstance(poolStock).AsSingle();

        }

        private void BindWorld() {
            
            Injector.Inject(this);

            // сущности
            entityStock = new EntityStock();
            Container.BindFromInstance(entityStock).AsSingle();
            
            #region Фабрики

            // звездный корабль
            entityStock.AddEntityFactory<Spaceship>(
                new SpaceshipEntityFactory(
                    entityStock, 
                    Container.Resolve<InputValuesPlayer>(), 
                    Container.Resolve<IFactory<SpaceshipObject>>(), 
                    SpaceshipMovementSettings,
                    SpaceshipWeaponSettings
                )
            );

            // астероиды
            // TODO: протащить через настройки параметры скорости космических объектов
            entityStock.AddEntityFactory<AsteroidBig>(
                new SpaceBodyEntityFactory<AsteroidBigComponent>(entityStock, events, sounds, AsteroidBigPrefab, 1f, 2f, SoundCase.AsteroidDestroyed, 50)
            );
            entityStock.AddEntityFactory<AsteroidMiddle>(
                new SpaceBodyEntityFactory<AsteroidMiddleComponent>(entityStock, events, sounds, AsteroidMiddlePrefab, 1.5f, 2.5f, SoundCase.AsteroidDestroyed, 60)
            );
            entityStock.AddEntityFactory<AsteroidSmall>(
                new SpaceBodyEntityFactory<AsteroidSmallComponent>(entityStock, events, sounds, AsteroidSmallPrefab, 2f, 3f, SoundCase.AsteroidSmallDestroyed, 100)
            );
            
            entityStock.AddEntityFactory<Ufo>(
                new UfoEntityFactory(entityStock, events, sounds, UfoSettings, UfoPrefab, 2f, 3f, SoundCase.UfoDestroyed, 475)
            );

            // пули
            // TODO: протащить через настройки параметры пуль
            entityStock.AddEntityFactory<BulletGun>(
                new BulletEntityFactory<BulletGun>(entityStock, poolStock, sounds, 15f, 0.6f, SoundCase.BulletGun, Damage.Normal)
            );
            entityStock.AddEntityFactory<BulletUfo>(
                new BulletEntityFactory<BulletUfo>(entityStock, poolStock, sounds, 5f, 2f, SoundCase.BulletUfo, Damage.Normal)
            );
            entityStock.AddEntityFactory<RayLaser>(
                new BulletEntityFactory<RayLaser>(entityStock, poolStock, sounds, 0f, 0.15f, SoundCase.Ray, Damage.Fatal)
            );

            // взрывы
            entityStock.AddEntityFactory<Explosion>(
                new ExplosionEntityFactory<Explosion>(entityStock, poolStock, 0.75f)
            );
            entityStock.AddEntityFactory<ExplosionSmall>(
                new ExplosionEntityFactory<ExplosionSmall>(entityStock, poolStock, 0.66f)
            );

            #endregion

            systemStock = new SystemStock(entityStock);
            Container.BindFromInstance(systemStock).AsSingle();

            #region Системы

            systemStock.AddSystem(new DestroySystem(entityStock));

            systemStock.AddSystem(new SpaceshipRotatingSystem());
            systemStock.AddSystem(new SpaceshipMovementSystem());
            systemStock.AddSystem(new SpaceshipCollisionSystem(entityStock, events, sounds));

            systemStock.AddSystem(new SpaceshipEngineAnimationSystem());
            systemStock.AddSystem(new InformationSystem(events));
            
            systemStock.AddSystem(new SpaceshipStandartShootingSystem(entityStock));
            systemStock.AddSystem(new SpaceshipImbalanceShootingSystem(entityStock));
            
            systemStock.AddSystem(new UfoAISystem(entityStock, Container.Resolve<SpaceshipObject>()));

            systemStock.AddSystem(new SpaceBodyMovementSystem());
            systemStock.AddSystem(new SpaceBodyCollisionSystem(entityStock, events, sounds));

            systemStock.AddSystem(new BulletCollisionSystem());

            systemStock.AddSystem(new ReleaseByTimeSystem(poolStock, entityStock));

            #endregion

        }
        
    }

}