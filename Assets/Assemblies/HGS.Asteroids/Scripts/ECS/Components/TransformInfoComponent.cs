using UnityEngine;

namespace HGS.Asteroids.ECS.Components {

    public class TransformInfoComponent: TransformBaseComponent {

        public TransformInfoComponent(Transform transform): base(transform) {

        }

        // промежуточное состояние
        public float speed { get; set; }

        public float time { get; set; }

        public Vector2 prevPosition { get; set; }

    }

}