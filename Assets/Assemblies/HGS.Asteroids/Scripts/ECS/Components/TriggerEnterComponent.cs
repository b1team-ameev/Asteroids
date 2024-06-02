using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Asteroids.ECS.Components {

    public class TriggerEnterComponent: EntityComponent {

        public TriggerEnterComponent(IEntity otherEntity): base(otherEntity) {

        }

    }

}