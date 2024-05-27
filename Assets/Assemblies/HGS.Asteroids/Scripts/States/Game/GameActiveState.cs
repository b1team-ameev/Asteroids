using System;
using HGS.Enums;
using HGS.Asteroids.Services;
using HGS.Asteroids.Games;
using HGS.Asteroids.States.Decisions;
using HGS.Tools.States;
using HGS.Tools.Commands;
using HGS.Tools.DI.Injection;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;

namespace HGS.Asteroids.States.StateGame {

    public class GameActiveState: GameState {

        private InputValuesUI inputValues;
        private Sounds sounds;
        private Events events;

        private int asteroidCount;
        private ActionCommand checkNewWaveCommand;

        private ActionCommand spawnUfoCommand;

        private readonly object lockObject = new object();

        [Inject]
        private void Constructor(Events events, InputValuesUI inputValues, Sounds sounds) {

            this.events = events;
            this.inputValues = inputValues;
            this.sounds = sounds;

        }

        public GameActiveState(StateMachine stateMachine) : base(stateMachine) {
            
            checkNewWaveCommand = new ExecuteActionAfterDelayCommand(CheckNewWave, 2f);

        }

        public override void Enter() {

            base.Enter();

            transitions.Add(new StateTransition(new EventDecision(EventKey.OnSpaceshipDestroyed), new GameLosedState(stateMachine)));

            events?.Add(EventKey.OnAsteroidSpawned, OnAsteroidSpawned);
            events?.Add(EventKey.OnAsteroidDestroyed, OnAsteroidDestroyed);

            OnEnter();

        }

        private void OnEnter() {

            sounds?.PlayBackground();

            AsteroidsGame game = stateMachine?.GetComponent<AsteroidsGame>();

            if (game != null) {
                
                game?.ShowControls();

                if (game.TimeInWaveForStartUfo > 0f) {

                    spawnUfoCommand = new ExecuteActionAfterDelayCommand(StartSpawnUfo, game.TimeInWaveForStartUfo);
                    spawnUfoCommand?.Execute();
                }

            }

        }

        public override void Exit(bool isFinal = false) {

            inputValues = null;

            checkNewWaveCommand?.Stop();
            spawnUfoCommand?.Stop();

            Game game = stateMachine?.GetComponent<Game>();

            if (game != null) {
                
                game?.HideControls();

            }

            events?.Remove(EventKey.OnAsteroidSpawned, OnAsteroidSpawned);
            events?.Remove(EventKey.OnAsteroidDestroyed, OnAsteroidDestroyed);

            base.Exit(isFinal);

        }

        public override void HandleInput() {

            base.HandleInput();

            if (inputValues != null && inputValues.IsPause) {

                stateMachine?.GetComponent<Game>()?.Pause();

            }

        }

        private void OnAsteroidSpawned(EventArgs e) {
            
            lock(lockObject) {

                asteroidCount++;

            }

            // UnityEngine.Debug.Log($"asteroidCount++ {this.asteroidCount}");

        }

        private void OnAsteroidDestroyed(EventArgs e) {
            
            var pE = e as UniversalEventArgs<int>;

            if (pE != null && pE.Value > 0) {

                stateMachine?.GetComponent<AsteroidsGame>()?.AddPoints(pE.Value);

            }

            lock(lockObject) {

                asteroidCount--;

                if (asteroidCount <= 0) {

                    checkNewWaveCommand?.Stop();
                    checkNewWaveCommand?.Execute();

                }

            }

            // UnityEngine.Debug.Log($"asteroidCount-- {this.asteroidCount}");

        }

        private void CheckNewWave() {
            
            lock(lockObject) {

                if (asteroidCount <= 0) {

                    asteroidCount = 0;

                    stateMachine?.GetComponent<AsteroidsGame>()?.NewWave();

                    sounds?.Play(SoundCase.NewWave);

                    spawnUfoCommand?.Stop();
                    spawnUfoCommand?.Execute();

                }

            }

        }

        private void StartSpawnUfo() {

            stateMachine?.GetComponent<AsteroidsGame>()?.SpawnUfo();

        }
        
    }

}