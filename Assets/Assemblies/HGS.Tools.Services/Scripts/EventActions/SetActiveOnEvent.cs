using System;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine;

namespace HGS.Tools.Services.EventActions {

    public class SetActiveOnEvent: ActionOnEvent {

        [field:SerializeField]
        public bool HideOnAwake { get; private set; }
        [field:SerializeField]
        public bool IsInversion { get; private set; }

        #region Awake/Start/Update/FixedUpdate

        protected override void Awake() {

            base.Awake();

            if (HideOnAwake) {
                
                this?.SetActive(false);

            }

        }

        #endregion

        protected override void OnEvent(EventArgs e) {

            var pE = e as UniversalEventArgs<bool>;

            // считаем, что если событие сработало, но оно не <bool>, значит isActive = true
            bool isActive = pE != null ? pE.Value : true;

            if (IsInversion) {

                isActive = !isActive;

            }

            if (this.IsActive() != isActive) {
                
                this?.SetActive(isActive);

            }

        }

    }

}