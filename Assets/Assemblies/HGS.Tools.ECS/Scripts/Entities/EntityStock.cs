using System;
using System.Collections.Generic;
using HGS.Tools.ECS.EntityFilters;

namespace HGS.Tools.ECS.Entities {

    public class EntityStock {

        private readonly List<IEntity> entities = new ();        
        private readonly List<IEntityFilter> entityFilters = new ();
        private readonly Dictionary<Type, IEntityFactory> entityFactories = new ();
        
        #if UNITY_EDITOR

        public IReadOnlyCollection<IEntity> EntitiesForEditor { get; private set; }

        #endif

        public EntityStock() {

            #if UNITY_EDITOR

            EntitiesForEditor = entities?.AsReadOnly();

            #endif

        }

        #region Entities

        public IEntity NewEntity() {

            IEntity entity = new Entity();

            lock(entities) {

                entities.Add(entity);

            }

            return entity;

        }

        public void RemoveEntity(IEntity entity) {

            if (entity == null) {
                
                return;

            }

            lock(entities) {

                if (entities.Contains(entity)) {

                    entities.Remove(entity);  

                }

            }

            OnEntityRemove(entity);
            (entity as IDisposable)?.Dispose();

        }

        // поиск осуществляется по установленному ранее в IEntity idObject
        public void RemoveEntity(object idObject) {

            IEntity entity = GetEntity(idObject);
            RemoveEntity(entity);

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

        #region EntityFilters

        internal void AddEntityFilter(IEntityFilter entityFilter) {

            if (entityFilter == null) {

                return;

            }

            lock(entityFilters) {

                entityFilters.Add(entityFilter);

            }

        }

        private void OnEntityRemove(IEntity entity) {
            
            if (entity == null) {
                
                return;

            }

            lock(entityFilters) {

                foreach(var entityFilter in entityFilters) {

                    entityFilter?.OnEntityRemove(entity);

                }

            }

        }

        // необходимо вызывать всегда, когда у entity изменился набор компонентов
        public void OnEntityUpdate(IEntity entity) {
            
            if (entity == null) {

                return;

            }

            lock(entityFilters) {

                foreach(var entityFilter in entityFilters) {

                    entityFilter?.OnEntityUpdate(entity);

                }

            }

        }

        #endregion

        public void Destroy() {

            lock(entities) {

                foreach(var entity in entities) {

                    (entity as IDisposable)?.Dispose();

                }

                entities.Clear();

            }

            lock(entityFactories) {

                foreach(var entityFactory in entityFactories) {

                    (entityFactory.Value as IDisposable)?.Dispose();

                }

                entityFactories.Clear();

            }

            lock(entityFilters) {

                entityFilters.Clear();

            }

        }

    }

}