using System;
using System.Collections.Generic;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public abstract class EntityFilter: IEntityFilter, IDisposable {
        
        private readonly List<EntityFiltered> entities = new ();

        private readonly List<IEntity> updatedEntities = new ();
        private readonly List<IEntity> removedEntities = new ();

        public IReadOnlyCollection<EntityFiltered> Entities { get; private set; }

        protected int componentCount = 0;

        public bool IsValid {

            get {

                return Entities != null && Entities.Count > 0;

            }

        }

        public EntityFilter() {

            Entities = entities.AsReadOnly();

        }

        protected abstract bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered);

        #region Работа с сущностями

        public void OnEntityRemove(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            lock(entities) {

                if (!removedEntities.Contains(entity)) {

                    removedEntities.Add(entity);

                }

            }

        }

        public void OnEntityUpdate(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            lock(entities) {

                if (!updatedEntities.Contains(entity)) {

                    updatedEntities.Add(entity);

                }

            }

        }

        #endregion

        #region Обновление данных

        private void EntityRemove(IEntity entity) {
            
            if (entity == null) {
                
                return;

            }

            int index = entities.FindIndex((entityFiltered) => entityFiltered?.Entity == entity);

            if (index >= 0) {

                EntityFiltered entityFiltered = entities[index];
                entityFiltered?.Dispose();

                entities.RemoveAt(index);           

            }

        }

        private void EntityUpdate(IEntity entity) {
            
            if (entity == null) {
                
                return;

            }

            EntityFiltered entityFiltered = new EntityFiltered(entity, componentCount);
            bool isEntityValid = CheckAndSetComponent(entity, entityFiltered);

            bool isNeedRemove = false;

            int index = entities.FindIndex((entityFiltered) => entityFiltered?.Entity == entity);

            // если сущность есть среди сущностей фильтра
            if (index >= 0) {

                if (isEntityValid) {

                    // ничего не делаем, но отмечаем, что локальную переменную надо почистить
                    isEntityValid = false;

                }
                else {

                    isNeedRemove = true;

                }

            }
            // если сущность подходит, сохраняем ее
            else if (isEntityValid) {

                entities.Add(entityFiltered);

            }

            if (isNeedRemove) {

                EntityRemove(entity);

            }

            if (!isEntityValid) {

                entityFiltered?.Dispose();

            }

        }

        public void Update() {

            if (updatedEntities.Count > 0 || removedEntities.Count > 0) {
            
                lock(entities) {

                    for(int i = 0; i < updatedEntities.Count; i++) {

                        IEntity entity = updatedEntities[i];

                        if (entity != null && removedEntities.Contains(entity)) {

                            updatedEntities.RemoveAt(i);

                        }

                    }

                    foreach(IEntity entity in updatedEntities) {

                        EntityUpdate(entity);

                    }

                    updatedEntities.Clear();

                    foreach(IEntity entity in removedEntities) {

                        EntityRemove(entity);

                    }

                    removedEntities.Clear();

                    for(int i = 0; i < entities.Count; i++) {

                        EntityFiltered entityFiltered = entities[i];

                        if (entityFiltered == null || entityFiltered.Entity == null) {

                            entities.RemoveAt(i);
                            entityFiltered?.Dispose();

                        }

                    }

                }

            }

        }

        #endregion

        #region Очистка данных

        public void Dispose() {

            lock(entities) {

                foreach(var entityFiltered in entities) {

                    entityFiltered?.Dispose();

                }

                entities.Clear();

                updatedEntities.Clear();
                removedEntities.Clear();

            }

        }

        #endregion

    }

}