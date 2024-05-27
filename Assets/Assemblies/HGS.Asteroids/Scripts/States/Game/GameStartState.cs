using HGS.Enums;
using HGS.Tools.States;
using HGS.Asteroids.Tools.Commands;

namespace HGS.Asteroids.States.StateGame {

    public class GameStartState: GameState {

        public GameStartState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            new SceneLoadCommand(Scene.Game, OnSceneLoaded).Execute();

        }

        private void OnSceneLoaded() {

            ChangeState(new GameActiveState(stateMachine));

        }

    }

}