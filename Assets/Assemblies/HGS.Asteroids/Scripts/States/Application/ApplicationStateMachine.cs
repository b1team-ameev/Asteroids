using HGS.Tools.States;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationStateMachine: StateMachine {
        
        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Initialize(new ApplicationStartState(this));

        }

        #endregion

    }

}