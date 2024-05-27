using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;
using UnityEngine.UI;
using HGS.Tools.Commands;

namespace HGS.Asteroids.Tools.UI {

    [RequireComponent(typeof(Button))]
    public class EventButton : InjectedMonoBehaviour {
        
        [field:SerializeField]
        private EventKey Event { get; set; }
        [field:SerializeField]
        private bool WithDelay { get; set; }

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();

            Button button = GetComponent<Button>();

            if (button != null) {

                button.onClick.AddListener(Execute);

            }

        }

        private void OnDestroy() {

            Button button = GetComponent<Button>();

            if (button != null) {

                button.onClick.RemoveListener(Execute);

            }

        }
        
        #endregion

        private void Execute() {
            
            if (Event != EventKey.Unknown) {

                if (WithDelay) {

                    new ExecuteActionAfterRealDelayCommand(() => {

                        events?.Raise(Event);

                    }).Execute();

                }
                else {

                    events?.Raise(Event);

                }

            }

        }

    }

}