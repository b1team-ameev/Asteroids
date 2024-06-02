using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.Components {

    public class EntityComponent: IComponent {

        public IEntity OtherEntity { get; private set; }

        public EntityComponent(IEntity otherEntity) {

            OtherEntity = otherEntity;

        }

    }

}