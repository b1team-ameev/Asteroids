using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFilter<T1, T2, T3>: EntityFilter<T1, T2> 
        where T1: IComponent
        where T2: IComponent
        where T3: IComponent {
        
        public EntityFilter(EntityStock entityStock): base(entityStock) {

            componentCount++;

        }

        protected override bool CheckAndSetComponent(IEntity entity, EntityFiltered entityFiltered) {

            IComponent component = entity.GetComponent<T3>();

            entityFiltered.Set(2, component);

            return component != null && base.CheckAndSetComponent(entity, entityFiltered);

        }

    }

}
