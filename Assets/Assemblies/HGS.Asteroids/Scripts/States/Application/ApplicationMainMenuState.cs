using HGS.Asteroids.States.Decisions;
using HGS.Enums;
using HGS.Tools.States;
using HGS.Asteroids.Tools.Commands;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationMainMenuState: ApplicationState {

        public ApplicationMainMenuState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new EventDecision(EventKey.OnAppGoToGameState), new ApplicationAsteroidsGameState(stateMachine)));
            
            # if !UNITY_EDITOR
            
            transitions.Add(new StateTransition(new EventDecision(EventKey.OnAppExit), new ApplicationExitState(stateMachine)));

            #endif

            OnEnter();

        }

        private void OnEnter() {

            new SceneLoadCommand(Scene.MainMenu, () => {

            }).Execute();

        }

    }

}