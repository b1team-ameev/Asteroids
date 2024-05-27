using UnityEngine;

namespace HGS.Asteroids.Behaviours {

    public class OnNeedDestroy: StateMachineBehaviour {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            if (animator != null && animator.gameObject != null) {

                Destroy(animator.gameObject); 

            }
            
        }

    }

}