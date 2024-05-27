using HGS.Enums;
using HGS.Tools.Services.ServiceSounds;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Tools.Services.ServiceSounds {

    public class SoundOnEvent: InjectedMonoBehaviour {
        
        [field:SerializeField]
        private SoundCase Sound { get; set; }

        private Sounds sounds;

        [Inject]
        private void Constructor(Sounds sounds) {

            this.sounds = sounds;

        }

        protected void Play() {

            if (sounds != null && Sound != SoundCase.Unknown) {

                sounds?.Play(Sound);

            }

        }

    }

}