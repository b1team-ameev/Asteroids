using System.Collections.Generic;

namespace HGS.Tools.States {

    public abstract class State<T>: State where T: StateMachine {

        protected List<StateTransition> transitions = new();

        public bool IsUseTransitions { get; protected set; }

        protected T stateMachine;

        protected State(StateMachine stateMachine): base() {

            this.stateMachine = (T)stateMachine;

            IsUseTransitions = true;

        }

        public override void Destroy() {

            base.Destroy();

            stateMachine = null;

            foreach(StateTransition transition in transitions.OrEmptyIfNull()) {
                transition.Destroy();
            }
            transitions?.Clear();

        }

        public override void LogicUpdate() {

            base.LogicUpdate();

            if (!IsUseTransitions) {
                return;
            }

            foreach(StateTransition transition in transitions.OrEmptyIfNull()) {

                if (transition.Execute(stateMachine)) {
                    return;
                }

            }

        }

        public virtual List<StateTransition> InitTransitionsByType() {

            return null;

        }

        protected virtual void ChangeState(State state) {

            if (IsExited) {
                return;
            }

            stateMachine?.ChangeState(state);

        }

    }

}