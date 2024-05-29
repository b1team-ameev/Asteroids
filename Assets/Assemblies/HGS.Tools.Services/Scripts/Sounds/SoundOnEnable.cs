namespace HGS.Tools.Services.ServiceSounds {

    public class SoundOnEnable: SoundOnEvent {
        
        #region Awake/Start/Update/FixedUpdate

        private void OnEnable() {

            Play();

        }

        #endregion

    }

}