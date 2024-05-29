using HGS.Asteroids.GameObjects;
using HGS.Asteroids.GameObjects.Weapons;
using HGS.Asteroids.States.Decisions;
using HGS.Asteroids.Services;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;
using HGS.Asteroids.GameObjects.Informers;
using HGS.Tools.Services.ServiceEvents;

namespace HGS.Asteroids.States.StateSpaceship {

    public class SpaceshipActiveState: SpaceshipState {

        private InputValuesPlayer inputValues;

        private Vector2 move;

        private bool isFired;
        private bool isAltFired;

        private Events events;

        private IInformer informerBulletCount;
        private IInformer informerTimeBeforeReload;
        private IInformer informerTransform;

        [Inject]
        private void Constructor(Events events, InputValuesPlayer inputValues) {

            this.events = events;
            this.inputValues = inputValues;

        }

        public SpaceshipActiveState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new DamageNormalDecision(), new SpaceshipDamagedState(stateMachine)));
            transitions.Add(new StateTransition(new DamageFatalDecision(), new SpaceshipDamagedState(stateMachine)));

            informerBulletCount = new InformerBulletCount(events, stateMachine?.GetComponent<ILimitedSupplyWeapon>());
            informerTimeBeforeReload = new InformerTimeBeforeReload(events, stateMachine?.GetComponent<IReloadableWeapon>());
            informerTransform = new InformerTransform(events, stateMachine?.Base());

        }

        public override void Exit(bool isFinal = false) {

            inputValues = null;
            events = null;

            informerBulletCount?.Clear();
            informerTimeBeforeReload?.Clear();
            informerTransform?.Clear();

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

            informerBulletCount?.ShowInfo();
            informerTimeBeforeReload?.ShowInfo();
            informerTransform?.ShowInfo();

            Spaceship spaceshipEngine = stateMachine?.GetComponent<Spaceship>();

            if (spaceshipEngine == null) {

                return;

            }

            // стрельба
            if (isAltFired) {

                IImbalanceWeapon weapon = stateMachine?.GetComponent<IImbalanceWeapon>();

                if (weapon != null && weapon.CanShoot()) {

                    weapon.Shoot(spaceshipEngine.FlightDirection);

                }

                isAltFired = false;
                
            }
            else if (isFired) {

                IStandartWeapon weapon = stateMachine?.GetComponent<IStandartWeapon>();

                if (weapon != null && weapon.CanShoot()) {

                    weapon.Shoot(spaceshipEngine.FlightDirection);

                }

                isFired = false;
            }

        }

        public override void PhysicsUpdate() {

            base.PhysicsUpdate();
            
            // движение
            Spaceship spaceshipEngine = stateMachine?.GetComponent<Spaceship>();

            if (spaceshipEngine != null) {

                spaceshipEngine.SetRotateDirection(move.x);
                spaceshipEngine.SetFlighting(move.y == 1f);

                spaceshipEngine.Rotate();
                spaceshipEngine.Fly();

            }
            
        }

    }

}