using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class ReleaseByTimeComponent: IComponent {

        public float TimeBeforeReleasing { get; private set; }

        public ReleaseByTimeComponent(float timeBeforeReleasing) {

            TimeBeforeReleasing = timeBeforeReleasing;

        }

        // промежуточное состояние
        public float CurrentTime { get; set; }

    }

}