using System.Collections.Generic;

namespace HGS.Tools.ECS.EntityFilters {

    public interface IEntityFilter {
        
        public bool IsValid { get; }
        public IReadOnlyCollection<EntityFiltered> Entities { get; }
        public EntityFilterState EntityFilterState { get; }

    }

}
