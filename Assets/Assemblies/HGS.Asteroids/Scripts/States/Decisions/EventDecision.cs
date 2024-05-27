using System;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using HGS.Tools.States;

namespace HGS.Asteroids.States.Decisions {

    public class EventDecision: StateDecisionInjected {

        private EventKey EventType { get; set; }
        private bool isEvent;

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        public EventDecision(EventKey eventType) {
            
            EventType = eventType;

            if (EventType != EventKey.Unknown) {

                events?.Add(EventType, OnEvent);

            }

        }

        public override void Destroy() {

            if (EventType != EventKey.Unknown) {

                events?.Remove(EventType, OnEvent);

            }

        }

        private void OnEvent(EventArgs e) {

            isEvent = true;

        }

        public override bool Decide(StateMachine stateMachine) {

            return isEvent;

        }

    }

}