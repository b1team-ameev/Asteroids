using HGS.Asteroids.States.Decisions;
using HGS.Tools.States;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using HGS.Enums;
using HGS.Asteroids.GameObjects;
using UnityEngine;

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

            Asteroid asteroid = stateMachine?.GetComponent<Asteroid>();

            if (asteroid != null) {

                asteroid.RandomRotate();

            }

        }

        public override void PhysicsUpdate() {

            base.PhysicsUpdate();
            
            // движение
            Asteroid asteroid = stateMachine?.GetComponent<Asteroid>();

            if (asteroid != null) {

                asteroid.Move();

            }

        }

    }

}