using HGS.Tools.States;

namespace HGS.Asteroids.States.StateGame {

    public class GameState: StateInjected<GameStateMachine> {

        public GameState(StateMachine stateMachine): base(stateMachine) {
            
        }

    }

}