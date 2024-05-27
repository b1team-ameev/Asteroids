using System;
using HGS.Tools.Services.ServiceEvents;
using UnityEngine.UI;

namespace HGS.Tools.Services.EventActions {

    public class PrintTextOnEvent: ActionOnEvent {
        
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

            UniversalEventArgs<string> pE = e as UniversalEventArgs<string>;

            if (pE != null && text != null) {
                
                text.text = pE.Value;

            }

        }
        
    }

}