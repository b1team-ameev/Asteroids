using UnityEngine;
using HGS.Asteroids.Enums;

namespace HGS.Asteroids.GameObjects.Damagers {

    public class DamageStatus: MonoBehaviour {
        
        [field:SerializeField]
        public Damage DamageReceived { get; private set; }

        #region OnCollisionEnter2D, OnCollisionExit2D, OnCollisionStay2D, OnTriggerEnter2D, OnTriggerExit2D, OnTriggerStay2D

        private void OnTriggerEnter2D(Collider2D other) {

            if (other == null) {

                return;

            }

            IDamager damager = other.GetComponent<IDamager>();

            if (damager != null) {

                DamageReceived = damager.Damage;

            }

        }

        #endregion

        public void ResetStatus() {

            DamageReceived = Damage.None;

        }

    }

}