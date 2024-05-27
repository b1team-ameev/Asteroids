using System;

namespace HGS.Tools.Services.EventActions {

    public class DestroyOnEvent: ActionOnEvent {
        
        protected override void OnEvent(EventArgs e) {

            Destroy(gameObject);

        }
        
    }

}