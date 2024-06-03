using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class SpaceshipObject {

        private Object prefab;
        private GameObject gameObject;

        public GameObject GameObject { 
            
            get {

                if (prefab != null && gameObject == null) {

                    gameObject = Object.Instantiate(prefab) as GameObject;

                }

                return gameObject;

            }
            
        }
        
        public SpaceshipObject(Object prefab) { 

            this.prefab = prefab;

        }

    }

}