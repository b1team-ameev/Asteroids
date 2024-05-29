using HGS.Tools.States;

namespace HGS.Asteroids.States.StateBullet {

    public class BulletStateMachine: StateMachine {

        #region Awake/Start/Update/FixedUpdate

        private void OnEnable() {

            Initialize(new BulletActiveState(this));

        }

        #endregion

    }

}