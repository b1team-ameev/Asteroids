using HGS.Tools.States;

namespace HGS.Asteroids.States.StateAsteroid {

    public class AsteroidStateMachine: StateMachine {

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Initialize(new AsteroidActiveState(this));

        }

        #endregion

    }

}