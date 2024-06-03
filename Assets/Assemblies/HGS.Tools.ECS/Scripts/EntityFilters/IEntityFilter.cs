using System.Collections.Generic;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public interface IEntityFilter {
        
        public bool IsValid { get; }
        public IReadOnlyCollection<EntityFiltered> Entities { get; }

        void EntityRemove(IEntity entity);
        void EntityUpdate(IEntity entity);
    }

}
