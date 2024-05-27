using UnityEngine;
using UnityEngine.InputSystem;

namespace HGS.Asteroids.Services {

    public class InputValuesUI: MonoBehaviour {

        [field:SerializeField]
        public bool IsPause { get; private set; }
        private bool IsPausePressed;

        private HGS.Tools.InputValues inputValues;

        #region Awake/Start/Update/FixedUpdate

        private void Awake() {

            inputValues = new HGS.Tools.InputValues();

        }

        private void Update() {

            IsPause = IsPausePressed;            
            IsPausePressed = false;

        }

        private void OnEnabled() {

            inputValues?.Enable();

        }

        private void OnDisable() {

            inputValues?.Disable();

        }

        #endregion

        private void OnPause(InputValue inputValue) {

            IsPausePressed = inputValue.isPressed;

        }

    }

}