using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class DestroyByTimeComponent: IComponent {

        public float TimeBeforeDestruction { get; private set; }

        public DestroyByTimeComponent(float timeBeforeDestruction) {

            TimeBeforeDestruction = timeBeforeDestruction;

        }

        // промежуточное состояние
        public float CurrentTime { get; set; }

    }

}