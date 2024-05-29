using System;
using HGS.Enums;
using HGS.Tools.States;
using HGS.Tools.Commands;

namespace HGS.Asteroids.States.Decisions {

    public class TimerDecision: StateDecision {

        private bool isTimeOver;
        private ActionCommand timerCommand;

        public TimerDecision(float time) {
            
            timerCommand = new ExecuteActionAfterDelayCommand(OnTimeOver, time);
            timerCommand?.Execute();

        }

        public override void Destroy() {

            timerCommand?.Stop();

        }

        private void OnTimeOver() {

            isTimeOver = true;

        }

        public override bool Decide(StateMachine stateMachine) {

            return isTimeOver;

        }

    }

}