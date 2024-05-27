using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;

namespace HGS.Asteroids.States.StateGame {

    public class GameLosedState: GameState {

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        public GameLosedState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            events?.Raise(EventKey.OnGameLosed);

        }

    }

}