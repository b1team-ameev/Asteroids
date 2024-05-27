using UnityEngine;

namespace HGS.Tools.Components {

    public class DestroyByTime: MonoBehaviour {

        [field:SerializeField]
        private float TimeBeforeDestruction { get; set; }
        
        #region Awake/Start/Update/FixedUpdate

        private void Start() {

            Destroy(gameObject, TimeBeforeDestruction);

        }

        #endregion

    }

}