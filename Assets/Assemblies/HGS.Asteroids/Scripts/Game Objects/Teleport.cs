using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Teleport: MonoBehaviour {
        
        [field:SerializeField]
        public Vector2 PositionFactor { get; private set; }
        [field:SerializeField]
        public Vector2 Delta { get; private set; }

        #region OnCollisionEnter2D, OnCollisionExit2D, OnCollisionStay2D, OnTriggerEnter2D, OnTriggerExit2D, OnTriggerStay2D

        private void OnTriggerEnter2D(Collider2D other) {

            if (other == null) {

                return;

            }

            other.transform.position *= PositionFactor;
            other.transform.position += (Vector3)Delta;

        }

        #endregion

    }

}