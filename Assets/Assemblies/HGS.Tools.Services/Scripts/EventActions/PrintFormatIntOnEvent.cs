using System;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine;
using UnityEngine.UI;

namespace HGS.Tools.Services.EventActions {

    public class PrintFormatIntOnEvent: ActionOnEvent {
        
        [field:SerializeField]
        public string Format { get; private set; }

        #region Компоненты

        private Text text;

        #endregion

        #region Awake/Start/Update/FixedUpdate

        protected override void Awake() {

            base.Awake();

            text = GetComponent<Text>();

        }

        #endregion

        protected override void OnEvent(EventArgs e) {

            UniversalEventArgs<int> pE = e as UniversalEventArgs<int>;

            if (pE != null && text != null) {
                
                text.text = pE.Value.ToString(Format);

            }

        }
        
    }

}
