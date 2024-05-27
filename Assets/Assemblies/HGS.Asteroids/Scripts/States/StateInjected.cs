

using HGS.Tools.DI.Injection;
using HGS.Tools.States;

namespace HGS.Asteroids.States {

    public abstract class StateInjected<T>: State<T> where T: StateMachine {

        protected StateInjected(StateMachine stateMachine): base(stateMachine) {

        }

        public override void Enter() {

            Injector.Inject(this);
            
        }

    }

}