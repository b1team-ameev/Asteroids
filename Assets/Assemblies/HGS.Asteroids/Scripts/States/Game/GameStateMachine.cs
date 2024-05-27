using HGS.Tools.States;

namespace HGS.Asteroids.States.StateGame {

    public class GameStateMachine: StateMachine {

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Initialize(new GameStartState(this));

        }

        #endregion

    }

}