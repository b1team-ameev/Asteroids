using System;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine;
using UnityEngine.UI;

namespace HGS.Tools.Services.EventActions {

    public class PrintFormatFloatOnEvent: ActionOnEvent {
        
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

            UniversalEventArgs<float> pE = e as UniversalEventArgs<float>;

            if (pE != null && text != null) {
                
                text.text = pE.Value.ToString(Format);

            }

        }
        
    }

}
