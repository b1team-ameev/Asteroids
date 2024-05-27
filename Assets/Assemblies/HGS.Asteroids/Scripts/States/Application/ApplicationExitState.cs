using HGS.Asteroids.Services;
using HGS.Tools.States;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationExitState: ApplicationState {

        public ApplicationExitState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            stateMachine?.GetComponent<GameApplication>()?.Exit();

        }

    }

}