using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Asteroid: MonoBehaviour {

        [field:SerializeField]
        public float MinSpeed { get; private set; }
        [field:SerializeField]
        public float MaxSpeed { get; private set; }

        private Transform thisTransform;

        private float speed;

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            speed = Random.Range(MinSpeed, MaxSpeed);

            thisTransform = transform;

        }

        #endregion

        public void RandomRotate() {

            if (thisTransform != null) {

                thisTransform.Rotate(new Vector3(0f, 0f, Random.value * 360f));

            }

        }

        public void Move() {

            if (thisTransform != null) {

                thisTransform.Translate(Vector2.up * speed * Time.fixedDeltaTime);

            }

        }

    }

}