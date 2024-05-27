namespace HGS.Tools.States {

    public abstract class StateDecision {

        public abstract bool Decide(StateMachine stateMachine);
        public virtual void Destroy() { }

    }

}