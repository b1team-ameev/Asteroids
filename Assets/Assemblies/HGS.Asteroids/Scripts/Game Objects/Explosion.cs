using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Explosion: MonoBehaviour, IExplosion {
        
        [field:SerializeField]
        private GameObject Prefab { get; set; }

        public void Exploud() {

            if (Prefab != null) {

                Transform thisTransform = transform;

                if (thisTransform != null) {

                    Instantiate(Prefab, thisTransform.position, Quaternion.identity, thisTransform.parent);

                }

            }

        }

    }

}