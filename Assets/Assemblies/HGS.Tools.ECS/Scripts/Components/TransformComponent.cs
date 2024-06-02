using UnityEngine;

namespace HGS.Tools.ECS.Components {

    public class TransformComponent: IComponent {

        public Transform Transform { get; private set; }

        public TransformComponent(Transform transform) {

            Transform = transform;

        }

    }

}
