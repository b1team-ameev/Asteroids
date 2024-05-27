using UnityEngine;

namespace HGS.Tools.States {

    public abstract class State {

        public bool IsExited { get; protected set; }
        public bool IsReady { get; protected set; }

        protected State() {

        }

        public virtual State GetThis() {
            
            return this;

        }

        public virtual void Enter() {
            
        }

        public virtual void Exit(bool isFinal = false) {

        }

        public virtual void Destroy() {

        }

        public virtual void OnBeforeExit() {
            
            IsExited = true;

        }

        public virtual void OnAfterEnter() {

            IsReady = true;

        }

        public virtual void HandleInput() {

        }

        public virtual void LogicUpdate() {

        }

        public virtual void PhysicsUpdate() {

        }

        // public virtual void OnBecameVisible() {

        // }

        // public virtual void OnBecameInvisible() {

        // }

        public virtual void OnTriggerEnter2D(Collider2D other) {

        }

        public virtual void OnTriggerExit2D(Collider2D other) {

        }

        public virtual void OnCollisionEnter2D(Collision2D other) {

        }

        public virtual void OnCollisionExit2D(Collision2D other) {

        }
        
        // public virtual void OnMouseDown() {

        // }
        
        // public virtual void OnAnimationStateExit() {

        // }
        
        public virtual bool CanChangeCurrentState() {

            return true;

        }
        
        public virtual bool CanBeReset() {

            return false;

        }

    }

}