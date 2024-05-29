using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine;

namespace HGS.Asteroids.GameObjects.Informers {

    public class InformerTransform: IInformer {
        
        private const float DELTA_TIME_LIMIT = 0.1f;

        private Events events;
        private Transform transform;

        private float speed;
        private float time;
        private Vector2 prevPosition;

        public InformerTransform(Events events, Transform transform) {
            
            this.events = events;
            this.transform = transform;

        }

        public void ShowInfo() {
            
            if (transform != null) {

                Show(transform.position.x, transform.position.y, speed, transform.rotation.eulerAngles.z);
                
                // оптимизация вычисления скорости
                time += Time.deltaTime;

                if (time >= DELTA_TIME_LIMIT) {

                    speed = (prevPosition - (Vector2)transform.position).magnitude / time;
                    time = 0f;

                    prevPosition = transform.position; 

                }

            }

        }
        
        public void Clear() {
            
            Show();
            
        }

        private void Show(float x = default, float y = default, float speed = default, float angle = default) {

            events?.Raise(EventKey.OnShowCoordinates, string.Format("{0}; {1}", x.ToString("N1"), y.ToString("N1")));

            events?.Raise(EventKey.OnShowInstantSpeed, string.Format("{0,4:#0.0} units/s", speed));

            events?.Raise(EventKey.OnShowRotationAngle, string.Format("{0,5:##0.0}°", angle));

        }

    }

}