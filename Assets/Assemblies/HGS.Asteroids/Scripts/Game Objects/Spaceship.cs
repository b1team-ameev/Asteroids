using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Spaceship: MonoBehaviour {
        
        [field:SerializeField]
        [field:Tooltip("Угол поворота")]
        public int RotateAngle { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Мощность полета")]
        public float FlightPower { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Ускорение Мощности полета")]
        public AnimationCurve FlightPowerAcceleration { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Затухание Мощности полета")]
        public AnimationCurve FlightPowerDecay { get; private set; }

        // !!! требуется оптимизация решения
        [field:SerializeField]
        public AnimationCurve AntiFlightPowerAcceleration { get; private set; }
        [field:SerializeField]
        public AnimationCurve AntiFlightPowerDecay { get; private set; }

        private Animator animator;

        private Transform thisTransform;
        private Transform baseTransform;

        private float rotateDirection;

        private AnimationCurve currenCurve;
        private float timeAction;

        private Vector2 flightDirection;

        private bool isFlighting;

        public Vector2 FlightDirection {

            get {

                return baseTransform != null ? baseTransform.rotation * Vector2.up : Vector2.up;

            }

        }

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            animator = GetComponent<Animator>();

            thisTransform = transform;
            baseTransform = this.Base();

        }

        private void FixedUpdate() {

            Rotate();
            Fly();

        }

        #endregion

        private void AnimateFlight() {

            if (animator != null) {

                animator.SetFloat("IsFlighting", isFlighting ? 1f : 0f);

            }

        }

        public void Rotate(float rotateDirection) {

            this.rotateDirection = -rotateDirection;

        }

        private void Rotate() {

            if (baseTransform == null) {

                return;

            }

            baseTransform.Rotate(0f, 0f, RotateAngle * rotateDirection * Time.fixedDeltaTime);

        }

        public void Fly(bool isFlighting) {

            if (this.isFlighting != isFlighting) {

                this.isFlighting = isFlighting;

                // по текущему Value пытаемся найти Time на противоположной Cure с таким же Value для плавного движения
                float currentValue = currenCurve != null ? currenCurve.Evaluate(timeAction) : default;
                currenCurve = isFlighting ? FlightPowerAcceleration : FlightPowerDecay;

                if (currentValue > 0f && timeAction > 0f) {

                    AnimationCurve antiCurve = isFlighting ? AntiFlightPowerAcceleration : AntiFlightPowerDecay;

                    // float t2 = antiCurve.Evaluate(currentValue);
                    // float v2 = currenCurve.Evaluate(t2);
                    
                    // Debug.Log($"t1 {timeAction}; v1 {currentValue}; t2 {t2}; v2 {v2}");

                    timeAction = antiCurve.Evaluate(currentValue);

                }
                else {

                    timeAction = 0f;

                }

                // направление полета
                if (isFlighting && baseTransform != null) {

                    flightDirection = baseTransform.rotation * Vector2.up;

                }

                AnimateFlight();

            }
            
            // if (isFlighting && baseTransform != null) {

            //     flightDirection = baseTransform.rotation * Vector2.up;

            // }

        }

        private void Fly() {

            if (thisTransform == null || currenCurve == null) {

                return;

            }

            timeAction += Time.fixedDeltaTime;

            float actionPower = currenCurve.Evaluate(timeAction);

            if (actionPower == 0f) {

                return;

            }

            thisTransform.Translate(flightDirection * actionPower * FlightPower * Time.fixedDeltaTime);

        }

    }

}