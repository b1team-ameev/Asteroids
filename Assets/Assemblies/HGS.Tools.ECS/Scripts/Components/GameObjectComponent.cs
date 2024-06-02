using UnityEngine;

namespace HGS.Tools.ECS.Components {

    public class GameObjectComponent: IComponent {

        public GameObject GameObject { get; private set; }

        public GameObjectComponent(GameObject gameObject) {

            GameObject = gameObject;

        }

    }

}