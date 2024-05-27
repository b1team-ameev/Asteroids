using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public class Bullet: MonoBehaviour {

        [field:SerializeField]
        private float Speed { get; set; }

        private Transform thisTransform;
        
        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            thisTransform = transform;

        }

        private void FixedUpdate() {

            Move();

        }

        #endregion

        private void Move() {

            if (thisTransform == null) {

                return;

            }

            thisTransform.Translate(Vector2.up * Speed * Time.fixedDeltaTime);

        }

    }

}