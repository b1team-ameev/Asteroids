using HGS.Asteroids.GameObjects;
using HGS.Tools.Services.ServiceSounds;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.States;
using HGS.Tools.DI.Injection;
using UnityEngine;
using HGS.Enums;

namespace HGS.Asteroids.States.StateSpaceship {

    public class SpaceshipDamagedState: SpaceshipState {

        private Events events;
        private Sounds sounds;

        [Inject]
        private void Constructor(Events events, Sounds sounds) {

            this.events = events;
            this.sounds = sounds;

        }

        public SpaceshipDamagedState(StateMachine stateMachine) : base(stateMachine) {
           
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

            events?.Raise(EventKey.OnSpaceshipDestroyed);

            // удаление объекта
            if (stateMachine != null && stateMachine.gameObject != null) {

                Object.Destroy(stateMachine.gameObject);

            }

        }

    }

}