using System;
using System.Collections.Generic;
using System.Linq;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public abstract class EntityFilter: IEntityFilter, IDisposable {
        
        protected bool isFiltered;

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

            isFiltered = false;

        }

        #region Работа с сущностями

        private void FilterEntities() {

            if (isFiltered || entityStock == null) {

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

            isFiltered = true;

        }

        protected abstract bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered);

        public void EntityRemove(IEntity entity) {
            
            int index = FindEntity(entity);

            if (index >= 0) {

                IReadOnlyCollection<EntityFiltered> tempEntities = entities;

                if (tempEntities != null && index < tempEntities.Count) {

                    EntityFiltered entityFiltered = tempEntities.ElementAt(index);

                    List<EntityFiltered> entities = new List<EntityFiltered>(tempEntities);
                    entities.RemoveAt(index);

                    this.entities = entities.AsReadOnly();

                    entityFiltered?.Dispose();

                }

            }

        }

        public void EntityUpdate(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            EntityFiltered entityFiltered = new EntityFiltered(entity, componentCount);
            bool isEntityValid = CheckAndSetComponent(entity, entityFiltered);

            int index = FindEntity(entity);

            // если сущность есть среди сущностей фильтра
            if (index >= 0) {

                if (isEntityValid) {

                    // ничего не делаем, но отмечаем, что локальную переменную надо почистить
                    isEntityValid = false;

                }
                else {

                    EntityRemove(entity);

                }

            }
            // если сущность подходит, сохраняем ее
            else if (isEntityValid && entities != null) {

                List<EntityFiltered> entities = new List<EntityFiltered>(this.entities);
                entities.Add(entityFiltered);

                this.entities = entities.AsReadOnly();

            }

            if (!isEntityValid) {

                entityFiltered?.Dispose();

            }

        }

        private int FindEntity(IEntity entity) {

            if (entity == null || entities == null) {

                return -1;

            }

            IReadOnlyCollection<EntityFiltered> tempEntities = entities;

            for(int i = 0; i < tempEntities.Count; i++) {

                EntityFiltered tempEntity = tempEntities.ElementAt(i);

                if (tempEntity?.Entity == entity) {

                    return i;

                }

            }

            return -1;

        }

        #endregion

        #region Очистка данных

        public void Dispose() {

            entityStock = null;

            DestroyEntities();

        }

        private void DestroyEntities() {

            if (entities != null) {

                IReadOnlyCollection<EntityFiltered> tempEntities = entities;

                foreach(var entityFiltered in tempEntities) {

                    entityFiltered?.Dispose();

                }

            }

            entities = null;

        }

        #endregion

    }

}