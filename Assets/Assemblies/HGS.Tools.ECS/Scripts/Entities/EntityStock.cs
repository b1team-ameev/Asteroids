using System;
using System.Collections.Generic;
using HGS.Tools.ECS.EntityFilters;

namespace HGS.Tools.ECS.Entities {

    public class EntityStock {

        private readonly List<IEntity> entities = new ();        
        // TODO: необходимо предусмотреть более оптимальную систему обновления фильтров
        private readonly List<EntityFilterState> entityFilterStates = new ();
        
        private readonly Dictionary<Type, IEntityFactory> entityFactories = new ();

        private IReadOnlyCollection<IEntity> entitiesReadOnly;

        public IReadOnlyCollection<IEntity> Entities { 

            get {

                return entitiesReadOnly;

            }

        }

        #region Entities

        public IEntity NewEntity() {

            IEntity entity = new Entity();

            lock(entities) {

                entities.Add(entity);

                entitiesReadOnly = entities.AsReadOnly();

            }

            ClearEntityFilterStates();

            return entity;

        }

        public void RemoveEntity(IEntity entity, bool isNeedClearStates = false) {

            if (entity == null) {

                return;

            }

            lock(entities) {

                if (entities.Contains(entity)) {

                    entities.Remove(entity);
                    entity?.Destroy();

                    entitiesReadOnly = entities.AsReadOnly();

                    isNeedClearStates = true;

                }

            }

            if (isNeedClearStates) {

                ClearEntityFilterStates();

            }

        }

        // поиск осуществляется по установленному ранее в IEntity idObject
        public void RemoveEntity(object idObject, bool isNeedClearStates = false) {

            IEntity entity = GetEntity(idObject);
            RemoveEntity(entity, isNeedClearStates);

        }

        // поиск осуществляется по установленному ранее в IEntity idObject
        public IEntity GetEntity(object idObject) {

            IEntity entity = null;

            lock(entities) {

                foreach(var tempEntity in entities) {

                    if (tempEntity != null && tempEntity.IdObject == idObject) {

                        entity = tempEntity;
                        break;

                    }

                }

            }

            return entity;

        }

        #endregion

        #region EntityFactories

        public void AddEntityFactory<T>(IEntityFactory entityFactory) {

            Type factoryType = typeof(T);

            lock(entityFactories) {

                if (!entityFactories.ContainsKey(factoryType)) {

                    entityFactories.Add(factoryType, entityFactory);

                }

            }

        }

        public IEntityFactory GetEntityFactory(Type factoryType) {

            lock(entityFactories) {

                if (entityFactories.ContainsKey(factoryType)) {

                    return entityFactories[factoryType];

                }

            }

            return null;

        }

        public IEntityFactory GetEntityFactory<T>() {

            return GetEntityFactory(typeof(T));

        }

        #endregion

        #region EntityFilterStates

        internal void AddEntityFilterState(EntityFilterState entityFilterState) {

            if (entityFilterState == null) {

                return;

            }

            lock(entityFilterStates) {

                entityFilterStates.Add(entityFilterState);

            }

        }

        private void ClearEntityFilterStates() {
            
            lock(entityFilterStates) {

                foreach(var state in entityFilterStates) {

                    if (state != null) {

                        state.IsFiltered = false;

                    }

                }

            }

        }

        public void RefreshEntityFilters() {
            
            ClearEntityFilterStates();

        }

        #endregion

        public void Destroy() {

            lock(entities) {

                foreach(var entity in entities) {

                    entity?.Destroy();

                }

                entities.Clear();

            }

            lock(entityFactories) {

                foreach(var entityFactory in entityFactories) {

                    entityFactory.Value?.Destroy();

                }

                entityFactories.Clear();

            }

            entitiesReadOnly = null;

            lock(entityFilterStates) {

                entityFilterStates.Clear();

            }

        }

    }

}