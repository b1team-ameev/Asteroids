using HGS.Tools.DI.Injection;
using HGS.Tools.Services.Collections;
using UnityEngine;
using HGS.Enums;

namespace HGS.Tools.Services.ServiceSounds {

    public class Sounds: InjectedMonoBehaviour {

        [field:SerializeField]
        private AudioSource AudioSourcePrefab { get; set; }
        [field:SerializeField]
        private Transform Container { get; set; }
        
        [field:SerializeField]
        private Camera CameraTarget { get; set; }
        
        [field:SerializeField]
        private float Volume { get; set; }
        [field:SerializeField]
        private float BackgroundVolume { get; set; }

        private ICollection<AudioClip> soundCollection;

        private Vector3 audioPosition;

        [Inject]
        private void Constructor(ICollection<AudioClip> soundCollection) {

            this.soundCollection = soundCollection;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();

            DontDestroyOnLoad(gameObject);

        }

        private void Start() {

            audioPosition = CameraTarget != null ? CameraTarget.transform.position : (Camera.main != null ? Camera.main.transform.position : Vector3.zero);

        }

        #endregion

        private void Play(SoundCase sound, float volume, bool isLoop = false) {

            if (sound != SoundCase.Unknown) {

                AudioClip audioClip = soundCollection?.GetObject(sound.ToString());

                if (audioClip != null) {

                    if (!isLoop || AudioSourcePrefab == null) {

                        AudioSource.PlayClipAtPoint(audioClip, audioPosition, volume);

                    }
                    else {

                        AudioSource audioSource = Instantiate(AudioSourcePrefab, Container);

                        if (audioSource != null) {

                            audioSource.name = "Sound " + sound.ToString();

                            audioSource.clip = audioClip;
                            
                            audioSource.loop = isLoop;
                            audioSource.volume = volume;

                            audioSource.Play();

                        }

                    } 

                }

            }

        }

        public void Play(SoundCase sound, bool isLoop = false) {

            Play(sound, Volume, isLoop); 

        }

        public void PlayBackground() {

            Play(SoundCase.Background, BackgroundVolume, true); 

        }

    }

}