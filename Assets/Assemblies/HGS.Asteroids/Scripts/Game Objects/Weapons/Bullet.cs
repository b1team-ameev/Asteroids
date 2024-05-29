using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public class Bullet: MonoBehaviour {

        [field:SerializeField]
        private float Speed { get; set; }
        [field:SerializeField]
        public float TimeBeforeDestruction { get; private set; }

        private Transform thisTransform;
        
        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            thisTransform = transform;

        }

        #endregion

        public void Move() {

            if (thisTransform == null) {

                return;

            }

            thisTransform.Translate(Vector2.up * Speed * Time.fixedDeltaTime);

        }

    }

}