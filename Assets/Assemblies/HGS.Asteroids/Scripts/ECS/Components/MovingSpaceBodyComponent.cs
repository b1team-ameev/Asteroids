using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class SpaceBodyMovementComponent: IComponent {

        public float Speed { get; private set; }

        public SpaceBodyMovementComponent(float speed) {

            Speed = speed;

        }

    }

}