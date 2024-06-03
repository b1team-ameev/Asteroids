using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class SpaceshipObject {

        private Object prefab;

        public GameObject GameObject { get; private set; }
        
        public SpaceshipObject(Object prefab) { 

            this.prefab = prefab;

        }

        public GameObject GetGameObject() {

            if (prefab != null && GameObject == null) {

                GameObject = Object.Instantiate(prefab) as GameObject;

            }

            return GameObject;

        }

    }

}