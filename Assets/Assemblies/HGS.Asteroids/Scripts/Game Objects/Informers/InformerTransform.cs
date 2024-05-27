using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.GameObjects.Informers {

    public class InformerTransform: InjectedMonoBehaviour {
        
        private const float DELTA_TIME_LIMIT = 0.1f;

        private Transform thisTransform;

        private float speed;
        private float time;
        private Vector2 prevPosition;

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();

            thisTransform = transform;

        }
        
        private void Update() {

            ShowData();

        }
        
        private void OnDestroy() {

            Show();

        }

        #endregion

        private void ShowData() {
            
            if (thisTransform != null) {

                Show(thisTransform.position.x, thisTransform.position.y, speed, thisTransform.rotation.eulerAngles.z);
                
                // оптимизация вычисления скорости
                time += Time.deltaTime;

                if (time >= DELTA_TIME_LIMIT) {

                    speed = (prevPosition - (Vector2)thisTransform.position).magnitude / time;
                    time = 0f;

                    prevPosition = thisTransform.position; 

                }

            }

        }

        private void Show(float x = default, float y = default, float speed = default, float angle = default) {

            events?.Raise(EventKey.OnShowCoordinates, string.Format("{0}; {1}", x.ToString("N1"), y.ToString("N1")));

            events?.Raise(EventKey.OnShowInstantSpeed, string.Format("{0,4:#0.0} units/s", speed));

            events?.Raise(EventKey.OnShowRotationAngle, string.Format("{0,5:##0.0}°", angle));

        }

    }

}