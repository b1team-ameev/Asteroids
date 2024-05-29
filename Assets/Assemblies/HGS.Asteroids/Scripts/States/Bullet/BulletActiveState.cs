using HGS.Asteroids.GameObjects.Weapons;
using HGS.Asteroids.States.Decisions;
using HGS.Tools.States;
using UnityEngine;

namespace HGS.Asteroids.States.StateBullet {

    public class BulletActiveState: BulletState {

        public BulletActiveState(StateMachine stateMachine) : base(stateMachine) {
                        
        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new DamageNormalDecision(), new BulletDamagedState(stateMachine)));
            transitions.Add(new StateTransition(new DamageFatalDecision(), new BulletDamagedState(stateMachine)));

            Bullet bullet = stateMachine?.GetComponent<Bullet>();

            if (bullet != null) {

                transitions.Add(new StateTransition(new TimerDecision(bullet.TimeBeforeDestruction), new BulletDamagedState(stateMachine)));

            }

        }

        public override void PhysicsUpdate() {

            base.PhysicsUpdate();
            
            // движение
            Bullet bullet = stateMachine?.GetComponent<Bullet>();

            if (bullet != null) {

                bullet.Move();

            }

        }

    }

}