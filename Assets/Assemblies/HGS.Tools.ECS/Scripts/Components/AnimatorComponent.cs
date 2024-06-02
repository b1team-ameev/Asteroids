using UnityEngine;

namespace HGS.Tools.ECS.Components {

    public class AnimatorComponent: IComponent {

        public Animator Animator { get; private set; }

        public AnimatorComponent(Animator animator) {

            Animator = animator;

        }

    }

}
