using System;
using System.Collections.Generic;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFilter<T1>: EntityFilter where T1: IComponent {
        
        public EntityFilter() {

            componentCount++;

        }

        protected override bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered) {

            IComponent component = entity.GetComponent<T1>();

            entityFiltered.Set(0, component);

            return component != null;

        }

    }

}
