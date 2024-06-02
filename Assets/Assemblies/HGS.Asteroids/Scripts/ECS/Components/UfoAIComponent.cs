using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class UfoAIComponent: IComponent {

        public float ShootingProbability { get; private set; }
        public float AimedShootingProbability { get; private set; }
        public float ChangeMoveDirectionProbability { get; private set; }

        public UfoAIComponent(float shootingProbability, float aimedShootingProbability, float changeMoveDirectionProbability) {

            ShootingProbability = shootingProbability;
            AimedShootingProbability = aimedShootingProbability;
            ChangeMoveDirectionProbability = changeMoveDirectionProbability;

        }

    }

}