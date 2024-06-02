using HGS.Asteroids.Services;
using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class InputValuesPlayerComponent: IComponent {

        public InputValuesPlayer InputValuesPlayer { get; private set; }

        public InputValuesPlayerComponent(InputValuesPlayer inputValuesPlayer) {

            InputValuesPlayer = inputValuesPlayer;

        }

    }

}