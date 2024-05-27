using System;

namespace HGS.Tools.States {

    public class StateProxy: State<StateMachine> {
        
        private Func<State> action;

        public StateProxy(StateMachine stateMachine, Func<State> action): base(stateMachine) {
            
            this.action = action;

        }

        public override State GetThis() {
            
            return action?.Invoke();

        }

    }

}