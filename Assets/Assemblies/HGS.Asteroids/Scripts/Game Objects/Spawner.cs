using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Spawner: MonoBehaviour, ISpawner {
        
        [field:SerializeField]
        private GameObject[] Prefabs { get; set; }
        [field:SerializeField]
        private Transform Container { get; set; }

        [field:SerializeField]
        private bool IsSpawnOnStart { get; set; }
        [field:SerializeField]
        private bool IsUseSpawnPosition { get; set; }

        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            if (IsSpawnOnStart) {

                Spawn();

            }

        }

        #endregion

        public void Spawn() {

            if (Prefabs != null) {

                Transform thisTransform = transform;

                foreach(var prevab in Prefabs) {

                    if (prevab != null) {

                        Instantiate(prevab, !IsUseSpawnPosition || thisTransform == null ? Vector3.zero : thisTransform.position,
                            thisTransform.rotation, Container != null || thisTransform == null ? Container : thisTransform.parent);

                    }

                }

            }

        }

    }

}