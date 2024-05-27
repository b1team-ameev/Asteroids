using UnityEngine;

namespace HGS.Asteroids.Tools.Cameras {

    public class CameraAutoSize: MonoBehaviour {
        
        [field:SerializeField]
        private float MinWidth { get; set; }
        [field:SerializeField]
        private float MinHeight { get; set; }

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            AutoSize();

        }

        #endregion

        public void AutoSize() {

            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = MinWidth / MinHeight;

            if (screenRatio >= targetRatio) {

                Camera.main.orthographicSize = MinHeight / 2;

            } else {

                float differenceInSize = targetRatio / screenRatio;
                Camera.main.orthographicSize = MinHeight / 2 * differenceInSize;

            }

        }

    }

}