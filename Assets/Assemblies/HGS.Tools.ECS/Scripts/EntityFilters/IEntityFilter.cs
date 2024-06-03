using System.Collections.Generic;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public interface IEntityFilter {
        
        public bool IsValid { get; }
        public IReadOnlyCollection<EntityFiltered> Entities { get; }

        public void EntityRemove(IEntity entity);
        public void EntityUpdate(IEntity entity);
    }

}
