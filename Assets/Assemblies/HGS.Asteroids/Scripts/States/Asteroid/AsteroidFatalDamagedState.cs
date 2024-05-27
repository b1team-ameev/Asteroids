using HGS.Asteroids.GameObjects;
using HGS.Tools.Services.ServiceSounds;
using HGS.Tools.Services.ServiceEvents;
using HGS.Enums;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.States.StateAsteroid {

    public class AsteroidFatalDamagedState: AsteroidState {

        private Events events;
        private Sounds sounds;

        [Inject]
        private void Constructor(Events events, Sounds sounds) {

            this.events = events;
            this.sounds = sounds;

        }

        public AsteroidFatalDamagedState(StateMachine stateMachine) : base(stateMachine) {
            
        }

        public override void Enter() {

            base.Enter();

            // генерация взрыва
            IExplosion explosion = stateMachine?.GetComponent<IExplosion>(); 
            explosion?.Exploud();

            // звук взрыва
            ISoundCaseOnDestroy soundOnDestroy = stateMachine?.GetComponent<ISoundCaseOnDestroy>();
            
            if (soundOnDestroy != null) {

                sounds?.Play(soundOnDestroy.Sound);

            }

            // сохраняем очки
            IPoints points = stateMachine?.GetComponent<IPoints>(); 
            events?.Raise(EventKey.OnAsteroidDestroyed, new UniversalEventArgs<int>(points != null ? points.Value : default));

            // удаление объекта
            if (stateMachine != null && stateMachine.gameObject != null) {

                Object.Destroy(stateMachine.gameObject);

            }

        }

    }

}