using HGS.Asteroids.States.Decisions;
using HGS.Tools.States;

namespace HGS.Asteroids.States.StateBullet {

    public class BulletActiveState: BulletState {

        public BulletActiveState(StateMachine stateMachine) : base(stateMachine) {
            
            transitions.Add(new StateTransition(new DamageNormalDecision(), new BulletDamagedState(stateMachine)));
            transitions.Add(new StateTransition(new DamageFatalDecision(), new BulletDamagedState(stateMachine)));

        }

    }

}