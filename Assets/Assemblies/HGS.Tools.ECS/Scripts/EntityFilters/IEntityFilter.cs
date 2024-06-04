using System.Collections.Generic;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public interface IEntityFilter {
        
        public bool IsValid { get; }
        public IReadOnlyCollection<EntityFiltered> Entities { get; }
        
        public void Update();

        public void OnEntityRemove(IEntity entity);
        public void OnEntityUpdate(IEntity entity);

    }

}
