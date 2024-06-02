using UnityEngine;

namespace HGS.Asteroids.Settings {

    [CreateAssetMenu]
    public class SpaceshipWeaponSettings: ScriptableObject {
        
        [field:SerializeField]
        public float StandartWeaponTimeout { get; private set; }

        [field:SerializeField]
        public float ImbalanceWeaponTimeout { get; private set; }        
        [field:SerializeField]
        public int ImbalanceWeaponMaxBulletCount { get; private set; }        
        [field:SerializeField]
        public float ImbalanceWeaponReloadTime { get; private set; } 

    }

}