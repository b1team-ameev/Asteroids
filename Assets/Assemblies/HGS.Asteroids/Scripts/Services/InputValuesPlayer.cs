using UnityEngine;
using UnityEngine.InputSystem;

namespace HGS.Asteroids.Services {

    public class InputValuesPlayer: MonoBehaviour {

        [field:SerializeField]
        public bool IsFire { get; private set; }

        [field:SerializeField]
        public bool IsAltFire { get; private set; }
        private bool isAltFirePressed;
        
        [field:SerializeField]
        public Vector2 Move { get; private set; }
        public Vector2 MoveRaw { 
            
            get {

                return new Vector2(Move.x == 0f ? 0f : Mathf.Sign(Move.x), Move.y == 0f ? 0f : Mathf.Sign(Move.y));

            } 
            
        }

        private HGS.Tools.InputValues inputValues;

        private bool IsWork {

            get {

                return Time.timeScale == 1f;

            }

        }

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            inputValues = new HGS.Tools.InputValues();

        }

        private void Update() {

            IsAltFire = isAltFirePressed;            
            isAltFirePressed = false;

        }

        private void OnEnabled() {

            inputValues?.Enable();

        }

        private void OnDisable() {

            inputValues?.Disable();

        }

        #endregion

        private void OnFire(InputValue inputValue) {

            IsFire = IsWork && inputValue.isPressed;

        }

        private void OnAltFire(InputValue inputValue) {

            isAltFirePressed = IsWork && inputValue.isPressed;

        }

        private void OnMove(InputValue inputValue) {

            if (IsWork) {

                Move = inputValue.Get<Vector2>();

            }
            else {

                Move = Vector2.zero;

            }

        }

    }

}