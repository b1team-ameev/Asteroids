using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class SpaceshipObject {

        public GameObject GameObject { get; private set; }
        
        private Object prefab;

        public SpaceshipObject(Object prefab) { 

            this.prefab = prefab;

        }

        public static explicit operator GameObject(SpaceshipObject spaceshipObject) {

            if (spaceshipObject.prefab != null && spaceshipObject.GameObject == null) {

                spaceshipObject.GameObject = Object.Instantiate(spaceshipObject.prefab) as GameObject;

            }

            return spaceshipObject.GameObject;

        }

    }

}