using UnityEngine;

namespace HGS.Asteroids.Settings {

    [CreateAssetMenu]
    public class UfoSettings: ScriptableObject {
        
        [field:SerializeField]
        public float StandartWeaponTimeout { get; private set; }

        [field:SerializeField]
        [field:Tooltip("Вероятнсоть выстрелить один раз в секунду")]
        public float AIShootingProbability { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Вероятнсоть, что выстрел при этом будет прицельным")]
        public float AIAimedShootingProbability { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Вероятность повернуть в сторону корабля один раз в секунду")]
        public float AIChangeMoveDirectionProbability { get; private set; }

    }

}