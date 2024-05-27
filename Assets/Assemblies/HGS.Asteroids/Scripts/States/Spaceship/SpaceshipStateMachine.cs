using HGS.Tools.States;

namespace HGS.Asteroids.States.StateSpaceship {

    public class SpaceshipStateMachine: StateMachine {

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Initialize(new SpaceshipActiveState(this));

        }

        #endregion

    }

}