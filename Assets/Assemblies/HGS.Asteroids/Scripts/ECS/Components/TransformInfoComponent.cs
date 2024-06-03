using UnityEngine;

namespace HGS.Asteroids.ECS.Components {

    public class TransformInfoComponent: TransformBaseComponent {

        public TransformInfoComponent(Transform transform): base(transform) {

        }

        // промежуточное состояние
        public float Speed { get; set; }

        public float Time { get; set; }

        public Vector2 PrevPosition { get; set; }

    }

}