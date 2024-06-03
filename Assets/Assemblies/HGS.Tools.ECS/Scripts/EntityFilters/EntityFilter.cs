using System;
using System.Collections.Generic;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFilter<T1>: IEntityFilter, IDisposable where T1: IComponent {
        
        public EntityFilterState EntityFilterState { get; private set; }

        protected EntityStock entityStock;
        private IReadOnlyCollection<EntityFiltered> entities;

        protected int componentCount = 0;

        public IReadOnlyCollection<EntityFiltered> Entities { 
            
            get {

                FilterEntities();

                return entities;

            } 
            
        }

        public bool IsValid {

            get {

                return Entities != null && Entities.Count > 0;

            }

        }

        public EntityFilter(EntityStock entityStock) {

            this.entityStock = entityStock;

            EntityFilterState = new EntityFilterState();

            componentCount++;

        }

        private void FilterEntities() {

            if (EntityFilterState.IsFiltered || entityStock == null) {

                return;

            }

            List<EntityFiltered> entities = new List<EntityFiltered>();

            foreach(var entity in entityStock.Entities) {

                if (entity != null) {

                    EntityFiltered entityFiltered = new EntityFiltered(entity, componentCount);

                    if (CheckAndSetComponent(entity, entityFiltered)) {

                        entities.Add(entityFiltered);

                    }
                    else {

                        (entityFiltered as IDisposable)?.Dispose();

                    }

                }

            }

            DestroyEntities();
            this.entities = entities.AsReadOnly();

            EntityFilterState.IsFiltered = true;

        }

        public void Dispose() {

            entityStock = null;

            DestroyEntities();

        }

        private void DestroyEntities() {

            if (entities != null) {

                IReadOnlyCollection<EntityFiltered> tempEntities = entities;

                foreach(var entityFiltered in tempEntities) {

                    (entityFiltered as IDisposable)?.Dispose();

                }

            }

            entities = null;

        }

        protected virtual bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered) {

            IComponent component = entity.GetComponent<T1>();

            entityFiltered.Set(0, component);

            return component != null;

        }

    }

}
