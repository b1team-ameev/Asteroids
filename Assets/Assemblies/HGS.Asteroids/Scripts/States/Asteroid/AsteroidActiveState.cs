using HGS.Asteroids.States.Decisions;
using HGS.Tools.States;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using HGS.Enums;

namespace HGS.Asteroids.States.StateAsteroid {

    public class AsteroidActiveState: AsteroidState {

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        public AsteroidActiveState(StateMachine stateMachine): base(stateMachine) {
            
        }
        
        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new DamageNormalDecision(), new AsteroidNormalDamagedState(stateMachine)));
            transitions.Add(new StateTransition(new DamageFatalDecision(), new AsteroidFatalDamagedState(stateMachine)));

            events?.Raise(EventKey.OnAsteroidSpawned);

        }

    }

}