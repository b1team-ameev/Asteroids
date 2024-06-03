using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Asteroids.ECS.Components;
using UnityEngine;
using HGS.Asteroids.ECS.Components.Damagers;
using HGS.Asteroids.Enums;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Asteroids.ECS.Entities;
using HGS.Asteroids.Settings;

namespace HGS.Asteroids.ECS.EntityFactories {

    public class UfoEntityFactory: IEntityFactory, System.IDisposable {

        private GameObject objectPrefab;
        private EntityStock entityStock;

        private Events events;
        private Sounds sounds;

        private readonly float minSpeed;
        private readonly float maxSpeed;
        
        public readonly SoundCase sound;

        private readonly int points;

        private UfoSettings ufoSettings;

        public UfoEntityFactory(
            EntityStock entityStock, Events events, Sounds sounds, 
            UfoSettings ufoSettings, GameObject objectPrefab, 
            float minSpeed, float maxSpeed, SoundCase sound, int points
        ) {

            this.entityStock = entityStock;

            this.events = events;
            this.sounds = sounds;

            this.ufoSettings = ufoSettings;
            this.objectPrefab = objectPrefab;

            this.minSpeed = minSpeed;
            this.maxSpeed = maxSpeed;

            this.sound = sound;
            
            this.points = points;

        }

        public IEntity Create() {
            
            IEntity entity = entityStock?.NewEntity();

            entity?.AddComponent<UfoComponent>();
            entity?.AddComponent(new SpaceBodyMovementComponent(Random.Range(minSpeed, maxSpeed)));

            entity?.AddComponent(new DamagerComponent(Damage.Fatal));
            
            if (sound != SoundCase.Unknown) {

                entity?.AddComponent(new SoundCaseComponent(sound));

            }

            if (points > 0) {

                entity?.AddComponent(new PointsComponent(points));

            }

            GameObject gameObject = Object.Instantiate(objectPrefab);

            if (gameObject != null) {

                entity?.SetIdObject(gameObject);
                entity?.AddComponent(new GameObjectComponent(gameObject));
                
                entity?.AddComponent(new TransformComponent(gameObject.transform));

                // поворот на случайный градус
                gameObject.transform.Rotate(new Vector3(0f, 0f, Random.value * 360f));

                // уведомление игры
                events?.Raise(EventKey.OnAsteroidSpawned);

                // навешиваем оружие на НЛО
                entity?.AddComponent(new GunComponent<BulletUfo>(ufoSettings.StandartWeaponTimeout));

                // навешиваем AI на НЛО
                entity?.AddComponent(
                    new UfoAIComponent(ufoSettings.AIShootingProbability, ufoSettings.AIAimedShootingProbability, ufoSettings.AIChangeMoveDirectionProbability)
                );

                // особый звук при появлении НЛО
                sounds?.Play(SoundCase.Ufo);

            }

            entityStock?.OnEntityUpdate(entity);
            
            return entity;

        }

        public void Dispose() {
            
            objectPrefab = null;
            entityStock = null;

            events = null;
            sounds = null;

        }
    }

}