using HGS.Asteroids.States.Decisions;
using HGS.Asteroids.Services;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using HGS.Tools.States;
using HGS.Asteroids.Tools.Commands;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationStartState: ApplicationState {

        private Events events;

        [Inject]
        private void Constructor(Events events) {

            this.events = events;

        }

        public ApplicationStartState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new EventDecision(EventKey.OnAppGoToMainMenuState), new ApplicationMainMenuState(stateMachine)));

            OnEnter();

        }

        private void OnEnter() {

            new SceneLoadCommand(Scene.Start, () => {

                stateMachine?.GetComponent<GameApplication>()?.Init();

                events?.Raise(EventKey.OnAppGoToMainMenuState);

            }).Execute();

        }

    }

}