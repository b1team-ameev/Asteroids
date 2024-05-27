using HGS.Tools.Components;
using UnityEngine;

namespace HGS.Tools.States {

    public class StateMachine: MonoBehaviourWithComponentCache {

        [field:SerializeField]
        public State CurrentState { get; private set; }

        #region Awake/Start/Update/FixedUpdate

        private void FixedUpdate() {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.PhysicsUpdate();

        }

        private void Update() {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.HandleInput();
            CurrentState?.LogicUpdate();

        }

        private void OnDestroy() {
            
            // прекращаем циклическую зависимость
            ChangeState(null);
            
        }

        #endregion

        #region OnCollisionEnter2D, OnCollisionExit2D, OnCollisionStay2D, OnTriggerEnter2D, OnTriggerExit2D, OnTriggerStay2D

        private void OnCollisionEnter2D(Collision2D other) {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.OnCollisionEnter2D(other);

        }

        private void OnTriggerExit2D(Collider2D other) {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.OnTriggerExit2D(other);

        }

        private void OnTriggerEnter2D(Collider2D other) {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.OnTriggerEnter2D(other);

        }
        
        private void OnCollisionExit2D(Collision2D other) {

            if (CurrentState == null || CurrentState.IsExited || !CurrentState.IsReady) {
                return;
            }

            CurrentState?.OnCollisionExit2D(other);

        }

        #endregion

        public void Initialize(State startingState) {

            ChangeState(startingState);

        }

        public void ChangeState(State newState) {

            // обеспечение работы StateProxy
            newState = newState?.GetThis();

            if (CurrentState != null && 
                (
                    (newState != null && CurrentState.IsExited) ||
                    (newState != null && newState.GetType() == CurrentState.GetType() && !newState.CanBeReset()) ||
                    (newState != null && !CurrentState.CanChangeCurrentState())
                )
            ) {
                return;
            }

            CurrentState?.OnBeforeExit();
            CurrentState?.Exit(newState == null);

            CurrentState?.Destroy();

            CurrentState = newState;

            CurrentState?.Enter();
            CurrentState?.OnAfterEnter();

        }

        public bool IsState<K>() where K: State {

            return CurrentState is K;

        }

    }

}