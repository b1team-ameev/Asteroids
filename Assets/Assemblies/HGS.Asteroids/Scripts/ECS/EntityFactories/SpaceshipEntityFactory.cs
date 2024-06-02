using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Asteroids.ECS.Entities;
using HGS.Asteroids.GameObjects;
using HGS.Asteroids.Services;
using HGS.Asteroids.Settings;
using HGS.Tools.DI.Factories;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.ECS.EntityFactories {

    public class SpaceshipEntityFactory: IEntityFactory {

        private EntityStock entityStock;

        private SpaceshipMovementSettings spaceshipMovementSettings;
        private SpaceshipWeaponSettings spaceshipWeaponSettings;

        private InputValuesPlayer inputValuesPlayer;

        private IFactory<SpaceshipObject> spaceshipFactory;

        public SpaceshipEntityFactory(
            EntityStock entityStock, 

            InputValuesPlayer inputValuesPlayer,
            IFactory<SpaceshipObject> spaceshipFactory,

            SpaceshipMovementSettings spaceshipMovementSettings, 
            SpaceshipWeaponSettings spaceshipWeaponSettings) {

            this.entityStock = entityStock;

            this.inputValuesPlayer = inputValuesPlayer;
            this.spaceshipFactory = spaceshipFactory;
            
            this.spaceshipMovementSettings = spaceshipMovementSettings;
            this.spaceshipWeaponSettings = spaceshipWeaponSettings;

        }

        public IEntity Create() {
            
            IEntity entity = entityStock?.NewEntity();

            entity?.AddComponent<SpaceshipComponent>();

            entity?.AddComponent(new SpaceshipRotatingComponent(spaceshipMovementSettings.RotateAngle));
            entity?.AddComponent(
                new SpaceshipMovementComponent(
                    spaceshipMovementSettings.FlightPower,
                    spaceshipMovementSettings.FlightPowerAcceleration,
                    spaceshipMovementSettings.FlightPowerDecay
                )
            );

            entity?.AddComponent(new GunComponent<BulletGun>(spaceshipWeaponSettings.StandartWeaponTimeout));
            entity?.AddComponent(
                new LaserComponent<RayLaser>(
                    spaceshipWeaponSettings.ImbalanceWeaponTimeout,
                    spaceshipWeaponSettings.ImbalanceWeaponMaxBulletCount,
                    spaceshipWeaponSettings.ImbalanceWeaponReloadTime
                )
            );

            entity?.AddComponent(new InputValuesPlayerComponent(inputValuesPlayer));

            // TODO: нужен гарантированный способ создания GameObject
            SpaceshipObject spaceshipObject = spaceshipFactory?.Create() as SpaceshipObject;
            GameObject gameObject = (GameObject)spaceshipObject;

            if (gameObject != null) {

                entity?.SetIdObject(gameObject);
                entity?.AddComponent(new GameObjectComponent(gameObject));

                entity.AddComponent(new TransformComponent(gameObject.transform));
                entity.AddComponent(new TransformInfoComponent(gameObject.transform));
                
                entity.AddComponent(new SpaceshipEngineAnimatorComponent(gameObject.GetComponent<Animator>()));
                
            }
            
            return entity;

        }

        public void Destroy() {
            
            inputValuesPlayer = null;
            spaceshipFactory = null;

            entityStock = null;

        }
    }

}