using HGS.Tools.ECS.Components;
using UnityEngine;

namespace HGS.Asteroids.ECS.Components {

    public class TransformBaseComponent: TransformComponent {

        public TransformBaseComponent(Transform transform): base(transform.Base()) {

        }

    }

}