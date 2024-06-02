using UnityEngine;

namespace HGS.Asteroids.Settings {

    [CreateAssetMenu]
    public class SpaceshipMovementSettings: ScriptableObject {
        
        [field:SerializeField]
        [field:Tooltip("Угол поворота")]
        public int RotateAngle { get; private set; }
        
        [field:SerializeField]
        [field:Tooltip("Мощность полета")]
        public float FlightPower { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Ускорение Мощности полета")]
        public AnimationCurve FlightPowerAcceleration { get; private set; }
        [field:SerializeField]
        [field:Tooltip("Затухание Мощности полета")]
        public AnimationCurve FlightPowerDecay { get; private set; }

    }

}