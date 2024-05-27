using HGS.Tools.States;
using UnityEngine;

namespace HGS.Asteroids.States.StateBullet {

    public class BulletDamagedState: BulletState {

        public BulletDamagedState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            // удаление объекта
            if (stateMachine != null && stateMachine.gameObject != null) {

                Object.Destroy(stateMachine.gameObject);

            }

        }

    }

}