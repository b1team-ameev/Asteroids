using HGS.Tools.States;
using UnityEngine;

namespace HGS.Asteroids.States.StateUfoAI {

    public class UfoAIStateMachine: StateMachine {

        [field:SerializeField]
        [field:Tooltip("Вероятнсоть выстрелить один раз в секунду")]
        public float ShootingProbability { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Вероятнсоть, что выстрел при этом будет прицельным")]
        public float AimedShootingProbability { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Вероятность повернуть в сторону корабля один раз в секунду")]
        public float ChangeMoveDirectionProbability { get; private set; }

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Initialize(new UfoAIActiveState(this));

        }

        #endregion

    }

}