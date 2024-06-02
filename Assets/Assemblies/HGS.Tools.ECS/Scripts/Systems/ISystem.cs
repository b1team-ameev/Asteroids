using HGS.Tools.ECS.EntityFilters;

namespace HGS.Tools.ECS.Systems {

    public interface ISystem {

        public IEntityFilter EntityFilter { get; }
        public void Run();

    }

}
