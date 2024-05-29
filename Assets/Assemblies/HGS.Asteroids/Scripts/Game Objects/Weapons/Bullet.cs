using UnityEngine;

namespace HGS.Asteroids.GameObjects.Weapons {

    public class Bullet: MonoBehaviour {

        [field:SerializeField]
        public float Speed { get; private set; }
        [field:SerializeField]
        public float TimeBeforeDestruction { get; private set; }

    }

}