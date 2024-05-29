using HGS.Asteroids.GameObjects.Damagers;
using HGS.Tools.DI.Injection;
using HGS.Tools.Services.Pools;
using HGS.Tools.States;

namespace HGS.Asteroids.States.StateBullet {

    public class BulletDamagedState: BulletState {

        private IPoolStack poolStack;

        [Inject]
        private void Constructor(IPoolStack poolStack) {
            
            this.poolStack = poolStack;

        }

        public BulletDamagedState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            // сброс статуса урона
            DamageStatus damageStatus = stateMachine?.GetComponent<DamageStatus>();

            if (damageStatus != null) {

                damageStatus.ResetStatus();

            }

            // удаление объекта
            if (stateMachine != null && stateMachine.gameObject != null) {

                // Object.Destroy(stateMachine.gameObject);
                poolStack?.Release(stateMachine.gameObject);

            }

        }

    }

}