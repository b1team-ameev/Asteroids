using HGS.Asteroids.GameObjects;
using HGS.Asteroids.GameObjects.Weapons;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.States.StateUfoAI {

    public class UfoAIActiveState: UfoAIState {

        private Spaceship spaceship;
        private Transform spaceshipTransform;
        
        private Transform thisTransform;

        [Inject]
        private void Constructor(Spaceship spaceship) {

            this.spaceship = spaceship;

        }

        public UfoAIActiveState(StateMachine stateMachine) : base(stateMachine) {    

        }

        public override void Enter() {

            base.Enter();
            
            if (spaceship != null) {

                spaceshipTransform = spaceship.transform;

            }  

            thisTransform = stateMachine?.transform;   

        }

        public override void LogicUpdate() {

            base.LogicUpdate();

            if (stateMachine == null || thisTransform == null) {

                return;

            }

            // стрельба
            if (Random.value < stateMachine.ShootingProbability * Time.deltaTime) {

                IWeapon weapon = stateMachine.GetComponent<IWeapon>();

                if (weapon != null) {

                    if (spaceshipTransform == null || !(Random.value < stateMachine.AimedShootingProbability)) {

                        weapon.Shoot(thisTransform.rotation * Vector2.up);

                    }
                    else {

                        // прицельный огонь
                        weapon.Shoot(spaceshipTransform.position - thisTransform.position);

                    }

                }

            }

            // смена направления
            if (Random.value < stateMachine.ChangeMoveDirectionProbability * Time.deltaTime) {

                if (spaceshipTransform != null) {

                    // на корабль
                    Vector2 moveVector = thisTransform.rotation * Vector2.up;
                    Vector2 spaceshipVector = spaceshipTransform.position - thisTransform.position;

                    float angle = Vector2.SignedAngle(moveVector, spaceshipVector);

                    // Debug.Log($"moveVector {moveVector}; spaceshipVector {spaceshipVector}; angle {angle}; ");

                    thisTransform.Rotate(0f, 0f, angle);

                }
                else {

                    thisTransform.Rotate(new Vector3(0f, 0f, Random.value * 360f));

                }

            }

        }

    }

}