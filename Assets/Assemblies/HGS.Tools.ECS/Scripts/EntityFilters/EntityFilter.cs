using System;
using System.Collections.Generic;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public abstract class EntityFilter: IEntityFilter, IDisposable {
        
        protected bool isFiltered;

        protected EntityStock entityStock;
        private readonly List<EntityFiltered> entities = new ();

        private IReadOnlyCollection<EntityFiltered> entitiesReadOnly;
        public IReadOnlyCollection<EntityFiltered> Entities { 
            
            get {

                FilterEntities();

                return entitiesReadOnly;

            } 
            
        }

        protected int componentCount = 0;

        public bool IsValid {

            get {

                return Entities != null && Entities.Count > 0;

            }

        }

        public EntityFilter(EntityStock entityStock) {

            this.entityStock = entityStock;

            isFiltered = false;

        }

        protected abstract bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered);

        #region Работа с сущностями

        private void FilterEntities() {

            if (isFiltered || entityStock == null) {

                return;

            }

            DestroyEntities();

            lock(entities) {

                if (entityStock.Entities != null) {

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

                    CreateReadOnlyCollection();

                }

            }

            isFiltered = true;

        }

        public void EntityRemove(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            lock(entities) {

                int index = entities.FindIndex((entityFiltered) => entityFiltered?.Entity == entity);

                if (index >= 0) {

                    EntityFiltered entityFiltered = entities[index];
                    entityFiltered?.Dispose();

                    entities.RemoveAt(index);    

                    CreateReadOnlyCollection();             

                }

            }

        }

        public void EntityUpdate(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            EntityFiltered entityFiltered = new EntityFiltered(entity, componentCount);
            bool isEntityValid = CheckAndSetComponent(entity, entityFiltered);

            bool isNeedRemove = false;

            lock(entities) {

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

                    CreateReadOnlyCollection();

                }

            }

            if (isNeedRemove) {

                EntityRemove(entity);

            }

            if (!isEntityValid) {

                entityFiltered?.Dispose();

            }

        }

        #endregion

        #region Очистка данных

        public void Dispose() {

            entityStock = null;

            DestroyEntities();

        }

        private void DestroyEntities() {

            lock(entities) {

                foreach(var entityFiltered in entities) {

                    entityFiltered?.Dispose();

                }

                entities.Clear();

                CreateReadOnlyCollection();

            }

            // entitiesReadOnly = null;

        }

        #endregion

        private void CreateReadOnlyCollection() {

            entitiesReadOnly = new List<EntityFiltered>(entities).AsReadOnly();

        }

    }

}