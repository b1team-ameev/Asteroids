using HGS.Enums;
using HGS.Asteroids.Games;
using HGS.Asteroids.States.Decisions;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.States.StateApplication {

    public class ApplicationAsteroidsGameState: ApplicationGameState {

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        public ApplicationAsteroidsGameState(StateMachine stateMachine): base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();
            
            transitions.Add(new StateTransition(new EventDecision(EventKey.OnAppGoToGameState), new ApplicationAsteroidsGameState(stateMachine)));

            events?.Add(EventKey.OnGameChangePauseState, OnGameChangePauseState);

        }
        
        public override void Exit(bool isFinal = false) {

            events?.Remove(EventKey.OnGameChangePauseState, OnGameChangePauseState);

            // сохраняем рекорд и т.д.

            base.Exit(isFinal);

        }

        private void OnGameChangePauseState(System.EventArgs e) {

            bool isPause = (bool)(e as UniversalEventArgs<bool>);

            // показываем-скрываем баннер

        }

        protected override void OnNewGame() {
            
            #region Реклама

            System.Action standartLogic = base.OnNewGame;

            // if (нужно показать рекламу) {

                // ...

            // }
            // else {

                standartLogic();

            // }

            #endregion

        }

        protected override Game CreateGame(GameObject gameObject) {

            if (gameObject == null) {
                
                return null;

            }

            AsteroidsGame game = gameObject.GetComponent<AsteroidsGame>();

            if (game != null) {

                // настройка непосредственно текущей игры

            }

            return game;

        }

        protected override ApplicationState CreateAfterGameState() {

            return new ApplicationMainMenuState(stateMachine);

        }

        public override bool CanBeReset() {

            return true;
            
        }

    }

}