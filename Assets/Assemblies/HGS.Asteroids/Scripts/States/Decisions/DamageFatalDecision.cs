using HGS.Asteroids.Enums;
using HGS.Asteroids.GameObjects.Damagers;
using HGS.Tools.States;

namespace HGS.Asteroids.States.Decisions {

    public class DamageFatalDecision: StateDecision {

        public override bool Decide(StateMachine stateMachine) {

            DamageStatus status = stateMachine?.GetComponent<DamageStatus>();

            return status != null && status.DamageReceived == Damage.Fatal;

        }

    }

}