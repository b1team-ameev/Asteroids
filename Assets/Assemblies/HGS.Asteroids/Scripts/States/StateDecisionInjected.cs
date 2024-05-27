using HGS.Tools.DI.Injection;
using HGS.Tools.States;

namespace HGS.Asteroids.States {

    public abstract class StateDecisionInjected: StateDecision {

        public StateDecisionInjected() {

            Injector.Inject(this);

        }

    }

}