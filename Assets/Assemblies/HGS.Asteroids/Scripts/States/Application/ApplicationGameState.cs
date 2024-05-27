using HGS.Asteroids.Games;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;
using HGS.Tools.States;
using HGS.Asteroids.States.Decisions;
using HGS.Enums;
using HGS.Asteroids.Services;

namespace HGS.Asteroids.States.StateApplication {

    public abstract class ApplicationGameState: ApplicationState {

        protected Game game;

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        public ApplicationGameState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();
            
            transitions.Add(new StateTransition(new EventDecision(EventKey.OnAppGoToAfterGameState), new StateProxy(stateMachine, CreateAfterGameStateProxy)));

            OnNewGame();

        }

        public override void Exit(bool isFinal = false) {

            if (game != null && game.gameObject != null) {

                Object.Destroy(game.gameObject);

            }

            ResetTimeScale();

            base.Exit(isFinal);

        }

        private void ResetTimeScale() {

            Time.timeScale = 1f;

        }

        protected virtual void InitGame() {

            GameApplication app = stateMachine?.GetComponent<GameApplication>();

            if (app != null) {

                GameObject gameObject = app.GetGameObject();
                game = CreateGame(gameObject);

            }

        }

        protected virtual void OnNewGame() {
            
            events?.Raise(EventKey.OnNewGame);

            InitGame();

        }

        protected virtual Game CreateGame(GameObject gameObject) {

            return null;

        }

        private ApplicationState CreateAfterGameStateProxy() {

            events?.Raise(EventKey.OnGameFinish);

            return CreateAfterGameState();

        }

        protected virtual ApplicationState CreateAfterGameState() {

            return null;

        }

    }

}