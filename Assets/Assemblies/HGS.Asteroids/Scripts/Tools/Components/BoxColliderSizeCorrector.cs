using System;
using UnityEngine;

namespace HGS.Asteroids.Tools.Components {

    [RequireComponent(typeof(BoxCollider2D))]
    public class BoxColliderSizeCorrector: MonoBehaviour {

        [field:SerializeField]
        private bool IsSetWidth { get; set; }
        [field:SerializeField]
        private bool IsSetHeight { get; set; }

        #region Awake/Start/Update/FixedUpdate

        private void Start() {
            
            SizeCorrect();

        }

        #endregion

        private void SizeCorrect() {

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            RectTransform parentTransform = transform.parent != null ? transform.parent.GetComponent<RectTransform>() : null;

            if (collider != null && parentTransform != null) {

                if (IsSetWidth) {

                    collider.size = new Vector2(parentTransform.rect.size.x, collider.size.y);

                }

                if (IsSetHeight) {

                    collider.size = new Vector2(collider.size.x, parentTransform.rect.size.y);

                }

            }
            
        }

    }

}