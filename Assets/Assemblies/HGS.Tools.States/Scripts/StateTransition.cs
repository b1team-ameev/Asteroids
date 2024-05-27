namespace HGS.Tools.States {

    public class StateTransition {
            
        protected StateDecision decision;
        protected State trueState;
        protected State falseState;

        public StateTransition(StateDecision decision, State trueState, State falseState = null) {

            this.decision = decision;
            this.trueState = trueState;
            this.falseState = falseState;

        }

        public bool Execute(StateMachine stateMachine) {

            if (decision == null) {
                return false;
            }

            if (decision.Decide(stateMachine)) {

                if (trueState != null) {

                    stateMachine?.ChangeState(trueState);
                    return true;

                }

            }
            else if (falseState != null) {

                stateMachine?.ChangeState(falseState);
                return true;

            }

            return false;
                
        }

        public void Destroy() {

            decision?.Destroy();
            decision = null;

            trueState = null;
            falseState = null;

        }

    }

}