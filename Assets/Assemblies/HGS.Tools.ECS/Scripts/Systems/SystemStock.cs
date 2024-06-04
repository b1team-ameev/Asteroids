using System;
using System.Collections.Generic;
using System.Reflection;
using HGS.Tools.ECS.Entities;
using HGS.Tools.ECS.EntityFilters;

namespace HGS.Tools.ECS.Systems {

    public class SystemStock {

        private EntityStock entityStock;
        private readonly List<ISystem> systems = new ();

        #if UNITY_EDITOR
        
        public IReadOnlyCollection<ISystem> SystemsForEditor { get; private set; }

        #endif

        public SystemStock(EntityStock entityStock) {

            this.entityStock = entityStock;

            #if UNITY_EDITOR
        
            SystemsForEditor = systems.AsReadOnly();

            #endif

        }

        #region Systems

        public void AddSystem(ISystem system) {

            lock(systems) {

                // для системы устанавливаем фильтр и передаем его в entityStock
                var properties = system.GetType().GetProperties();

                foreach(var property in properties) {
 
                    var attribute = property?.GetCustomAttribute(typeof(EntityFilterAttribute)) as EntityFilterAttribute;

                    if (attribute != null) {

                        Type filterType = attribute.FilterType;

                        if (filterType != null) {

                            // устанавливаем фильтр
                            property.SetValue(system, filterType.GetConstructor(Type.EmptyTypes)?.Invoke(null));

                            // передаем его в entityStock
                            IEntityFilter entityFilter = property.GetValue(system) as IEntityFilter;

                            if (entityFilter != null) {

                                entityStock?.AddEntityFilter(entityFilter);

                            }

                        }

                    }        

                }

                systems.Add(system);

            }

        }

        #endregion

        public void Run() {

            // TODO: поддержка многопоточности
            lock(systems) {

                foreach(var system in systems) {

                    system?.EntityFilter?.Update();

                    if (system != null && system.EntityFilter != null && system.EntityFilter.IsValid) {

                        system?.Run();

                    }

                }   

            }

        }

        public void Destroy() {

            lock(systems) {

                foreach(var system in systems) {

                    (system?.EntityFilter as IDisposable)?.Dispose();
                    (system as IDisposable)?.Dispose();

                }   

                systems.Clear();

            }

            entityStock = null;

        }

    }

}