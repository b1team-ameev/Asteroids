using HGS.Asteroids.GameObjects;
using HGS.Asteroids.GameObjects.Weapons;
using HGS.Asteroids.States.Decisions;
using HGS.Asteroids.Services;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.States.StateSpaceship {

    public class SpaceshipActiveState: SpaceshipState {

        private InputValuesPlayer inputValues;

        private Vector2 move;

        private bool isFired;
        private bool isAltFired;

        [Inject]
        private void Constructor(InputValuesPlayer inputValues) {

            this.inputValues = inputValues;

        }

        public SpaceshipActiveState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new DamageNormalDecision(), new SpaceshipDamagedState(stateMachine)));
            transitions.Add(new StateTransition(new DamageFatalDecision(), new SpaceshipDamagedState(stateMachine)));

        }

        public override void Exit(bool isFinal = false) {

            inputValues = null;

            base.Exit(isFinal);

        }

        public override void HandleInput() {

            base.HandleInput();

            move = inputValues != null ? inputValues.MoveRaw : default;

            isFired = isFired || inputValues != null ? inputValues.IsFire : false;
            isAltFired = isAltFired || inputValues != null ? inputValues.IsAltFire : false;

        }

        public override void LogicUpdate() {

            base.LogicUpdate();

            Spaceship spaceshipEngine = stateMachine?.GetComponent<Spaceship>();

            if (spaceshipEngine == null) {

                return;

            }

            // стрельба
            if (isAltFired) {

                IImbalanceWeapon weapon = stateMachine?.GetComponent<IImbalanceWeapon>();
                weapon.Shoot(spaceshipEngine.FlightDirection);

                isAltFired = false;
            }
            else if (isFired) {

                IStandartWeapon weapon = stateMachine?.GetComponent<IStandartWeapon>();
                weapon.Shoot(spaceshipEngine.FlightDirection);

                isFired = false;
            }

        }

        public override void PhysicsUpdate() {

            base.PhysicsUpdate();
            
            // движение
            Spaceship spaceshipEngine = stateMachine?.GetComponent<Spaceship>();

            if (spaceshipEngine != null) {

                spaceshipEngine.Rotate(move.x);
                spaceshipEngine.Fly(move.y == 1f);

            }

        }

    }

}