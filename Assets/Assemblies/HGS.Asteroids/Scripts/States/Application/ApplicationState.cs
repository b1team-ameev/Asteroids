using HGS.Tools.States;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationState: StateInjected<ApplicationStateMachine> {

        public ApplicationState(StateMachine stateMachine): base(stateMachine) { }

    }

}