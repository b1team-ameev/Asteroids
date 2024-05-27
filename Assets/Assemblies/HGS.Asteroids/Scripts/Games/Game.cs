using System;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.Games {

    public abstract class Game: InjectedMonoBehaviour {
        
        public bool IsPaused { get; private set; }
        private bool CanPaused { get; set; }

        protected Events events;
        protected Sounds sounds;

        [Inject]
        private void Constructor(Events events, Sounds sounds) {
            
            this.events = events;
            this.sounds = sounds;

        }

        #region Awake/Start/Update/FixedUpdate

        protected new virtual void Awake() {
            
            base.Awake();

            events?.Add(EventKey.OnAppPause, OnAppPause);

            DontDestroyOnLoad(this);

        }

        protected virtual void OnDestroy() {

            events?.Remove(EventKey.OnAppPause, OnAppPause);

        }

        #endregion

        #region Игровые события

        private void OnAppPause(EventArgs e) {

            bool isPause = (bool)(e as UniversalEventArgs<bool>);

            if (!IsPaused) {

                if (CanPaused) {

                    Pause(false);

                }
                else {

                    Time.timeScale = isPause ? 0f : 1f;

                }

            }

        }

        #endregion

        #region Пауза

        public void Pause(bool withSound = true) {

            if (!CanPaused) {

                return;

            }

            IsPaused = !IsPaused;

            if (IsPaused && withSound) {

                sounds?.Play(SoundCase.Pause);

            }

            Time.timeScale = IsPaused ? 0f : 1f;

            if (IsPaused) {

                // действия при постановке на паузу

            }
            else {

                // действия при снятии с паузы

            }

            events?.Raise(EventKey.OnGameChangePauseState, new UniversalEventArgs<bool>(IsPaused));

        }

        #endregion

        public void HideControls() {

            CanPaused = false;

            events?.RaiseDelayed(EventKey.OnGameChangeControlsState, new UniversalEventArgs<bool>(false));

        }

        public void ShowControls() {

            CanPaused = true;
            IsPaused = false;

            events?.RaiseDelayed(EventKey.OnGameChangeControlsState, new UniversalEventArgs<bool>(true));

        }

    }

}