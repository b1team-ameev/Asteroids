using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.ECS.Components.Damagers;
using HGS.Tools.DI.Injection;
using HGS.Tools.ECS.Entities;
using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class EntityDamageCollisionDetector: InjectedMonoBehaviour {

        private EntityStock entityStock;

        [Inject]
        private void Constructor(EntityStock entityStock) {

            this.entityStock = entityStock;

        }

        #region OnCollisionEnter2D, OnCollisionExit2D, OnCollisionStay2D, OnTriggerEnter2D, OnTriggerExit2D, OnTriggerStay2D

        private void OnTriggerEnter2D(Collider2D other) {

            if (other == null) {

                return;

            }

            IEntity entity = entityStock?.GetEntity(gameObject);
            IEntity otherEntity = entityStock?.GetEntity(other.gameObject);

            IDamagerComponent damagerComponent = otherEntity?.GetComponent<IDamagerComponent>();

            if (entity != null && otherEntity != null && damagerComponent != null) {
                
                entity?.AddComponent(new TriggerEnterComponent(otherEntity));
                entityStock?.OnEntityUpdate(entity);

            }

        }

        #endregion

    }

}