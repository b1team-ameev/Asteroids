using System;
using System.Collections;
using UnityEngine;

namespace HGS.Tools.Commands {

    public class ActionHelper: MonoBehaviour {

        #region Awake/Start/Update/FixedUpdate

        private void OnDestroy() {
            
            StopAllCoroutines();

        }

        #endregion

        public IEnumerator ActionAfterWaitTime(float waitForSecond, Action action) {
            
            yield return new WaitForSeconds(waitForSecond);

            if (gameObject != null && action != null) {

                action();

            }

        }

        public IEnumerator ActionAfterWaitRealTime(float waitForSecond, Action action) {
            
            yield return new WaitForSecondsRealtime(waitForSecond);

            if (gameObject != null && action != null) {

                action();

            }

        }

        public static ActionHelper CreateActionHelper(bool isDontDestroyOnLoad = false) {

            GameObject actionGameObject = new GameObject("Coroutine Action Helper" + (isDontDestroyOnLoad ? " DontDestroyOnLoad" : ""));
            
            if (actionGameObject != null) {

                if (isDontDestroyOnLoad) {

                    DontDestroyOnLoad(actionGameObject);

                }

                return actionGameObject.AddComponent<ActionHelper>();            

            }

            return null;

        }

        public static void DestroyActionHelper(ActionHelper actionHelper) {

            if (actionHelper != null && actionHelper.gameObject != null) {

                Destroy(actionHelper.gameObject); 

            }

        }
        
    }

}