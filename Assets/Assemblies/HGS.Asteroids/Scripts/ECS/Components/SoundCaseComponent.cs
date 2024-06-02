using HGS.Enums;
using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public class SoundCaseComponent: IComponent {

        public SoundCase Sound { get; private set; }

        public SoundCaseComponent(SoundCase sound) {

            Sound = sound;

        }

    }

}