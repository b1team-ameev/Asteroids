using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;
using HGS.Asteroids.ECS.Components;
using UnityEngine;
using HGS.Tools.Services.Pools;
using HGS.Asteroids.ECS.Components.Weapons;
using HGS.Tools.Services.ServiceSounds;
using HGS.Enums;
using HGS.Asteroids.Enums;
using HGS.Asteroids.ECS.Components.Damagers;
using System;

namespace HGS.Asteroids.ECS.EntityFactories {

    public class BulletEntityFactory<T>: IEntityFactory, IDisposable {

        private readonly float speed;
        public readonly float timeBeforeDestruction;
        public readonly SoundCase sound;
        public readonly Damage damage;

        private EntityStock entityStock;
        private PoolStock poolStock;
        
        private Sounds sounds;

        public BulletEntityFactory(
            EntityStock entityStock, PoolStock poolStock, Sounds sounds, 
            float speed, float timeBeforeDestruction, SoundCase sound, Damage damage) {

            this.poolStock = poolStock;
            this.entityStock = entityStock;
            
            this.sounds = sounds;

            this.speed = speed;
            this.timeBeforeDestruction = timeBeforeDestruction;

            this.sound = sound;

            this.damage = damage;

        }

        public IEntity Create() {
            
            IEntity entity = entityStock?.NewEntity();

            entity?.AddComponent<BulletComponent>();

            if (speed > 0f) {

                entity?.AddComponent(new SpaceBodyMovementComponent(speed));

            }
            if (timeBeforeDestruction > 0f) {

                entity?.AddComponent(new ReleaseByTimeComponent(timeBeforeDestruction));

            }
            if (damage != Damage.None) {

                entity?.AddComponent(new DamagerComponent(damage));

            }

            GameObject gameObject = poolStock?.Get<T>() as GameObject;

            if (gameObject != null) {

                entity?.SetIdObject(gameObject);
                entity?.AddComponent(new GameObjectComponent(gameObject));

                entity?.AddComponent(new TransformComponent(gameObject.transform));

                sounds?.Play(sound);

            }

            entityStock?.OnEntityUpdate(entity);
            
            return entity;

        }

        public void Dispose() {
            
            poolStock = null;
            entityStock = null;

            sounds = null;

        }
    }

}