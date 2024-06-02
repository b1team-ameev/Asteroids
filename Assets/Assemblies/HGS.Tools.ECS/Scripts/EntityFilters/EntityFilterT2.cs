using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFilter<T1, T2>: EntityFilter<T1> 
        where T1: IComponent
        where T2: IComponent {
        
        public EntityFilter(EntityStock entityStock): base(entityStock) {

            componentCount++;

        }

        protected override bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered) {

            IComponent component = entity.GetComponent<T2>();

            entityFiltered.Set(1, component);

            return component != null && base.CheckAndSetComponent(entity, entityFiltered);

        }

    }

}
