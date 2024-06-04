using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFilter<T1, T2, T3, T4>: EntityFilter<T1, T2, T3> 
        where T1: IComponent
        where T2: IComponent
        where T3: IComponent
        where T4: IComponent {
        
        public EntityFilter() {

            componentCount++;

        }

        protected override bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered) {

            IComponent component = entity.GetComponent<T4>();

            entityFiltered.Set(3, component);

            return component != null && base.CheckAndSetComponent(entity, entityFiltered);

        }

    }

}
