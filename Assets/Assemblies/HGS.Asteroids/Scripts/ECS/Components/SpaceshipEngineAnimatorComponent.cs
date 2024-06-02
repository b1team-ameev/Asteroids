using HGS.Tools.ECS.Components;
using UnityEngine;

namespace HGS.Asteroids.ECS.Components {

    public class SpaceshipEngineAnimatorComponent: AnimatorComponent {

        public SpaceshipEngineAnimatorComponent(Animator animator): base(animator) {

        }

        // промежуточное состояние
        public bool IsFlighting { get; set; }

    }

}