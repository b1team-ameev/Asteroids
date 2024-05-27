using System;
using UnityEngine;

namespace HGS.Tools.Services.EventActions {

    public class PlayAnimationOnEvent: ActionOnEvent {
        
        [field:SerializeField]
        public string Animation { get; private set; }

        #region Компоненты

        private Animator animator;

        #endregion

        #region Awake/Start/Update/FixedUpdate

        protected override void Awake() {

            base.Awake();

            animator = GetComponent<Animator>();

        }

        #endregion

        protected override void OnEvent(EventArgs e) {

            if (animator != null) {
                
                animator.Play(Animation);

            }

        }

    }

}