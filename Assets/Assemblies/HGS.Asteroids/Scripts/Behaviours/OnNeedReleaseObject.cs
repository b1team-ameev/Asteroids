using HGS.Tools.DI.Injection;
using HGS.Tools.Services.Pools;
using UnityEngine;

namespace HGS.Asteroids.Behaviours {

    public class OnNeedReleaseObject: StateMachineBehaviour {

        private IPoolStack poolStack;

        [Inject]
        private void Constructor(IPoolStack poolStack) {
            
            this.poolStack = poolStack;

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            if (animator != null && animator.gameObject != null) {

                Injector.Inject(this);

                poolStack?.Release(animator.gameObject); 

            }
            
        }

    }

}