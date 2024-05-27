using UnityEngine;
using HGS.Asteroids.Enums;

namespace HGS.Asteroids.GameObjects.Damagers {

    public class Damager: MonoBehaviour, IDamager {

        [field:SerializeField]
        public Damage Damage { get; private set; }

    }

}