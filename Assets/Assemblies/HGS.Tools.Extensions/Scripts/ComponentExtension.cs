using UnityEngine;
using UnityEngine.UI;

namespace HGS {

    public static class ComponentExtension {
        
        public static void SetActive(this Component component, bool isActive) {

            if (component.gameObject != null) {

                component.gameObject.SetActive(isActive);

            }

        }

        public static bool IsActive(this Component component) {

            if (component.gameObject != null) {

                return component.gameObject.activeSelf;

            }

            return false;

        }

        public static void SetText(this Component component, string value) {

            Text text = component as Text;

            if (text != null) {

                text.text = value;

            }

        }

        public static Transform Base(this Component component) {

            return component.transform.Find("Base");

        }

        public static void SetLayerName(this Component component, string layerName) {

            SpriteRenderer spriter = component.GetComponent<SpriteRenderer>();

            if (spriter != null) {

                spriter.sortingLayerName = layerName;

            }

        }

        public static string GetLayerName(this Component component) {

            SpriteRenderer spriter = component.GetComponent<SpriteRenderer>();

            if (spriter != null) {

                return spriter.sortingLayerName;

            }

            return string.Empty;

        }

    }

}