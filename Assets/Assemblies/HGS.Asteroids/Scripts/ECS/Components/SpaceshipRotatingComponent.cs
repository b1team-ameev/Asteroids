using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class SpaceshipRotatingComponent: IComponent {

        public int RotateAngle { get; private set; }

        public SpaceshipRotatingComponent(int rotateAngle) {

            RotateAngle = rotateAngle;

        }

    }

}