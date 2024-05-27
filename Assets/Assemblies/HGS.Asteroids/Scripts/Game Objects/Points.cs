using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Points: MonoBehaviour, IPoints {
        
        [field:SerializeField]
        public int Value { get; private set; }

    }

}