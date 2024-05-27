using UnityEngine;

namespace HGS.Tools.Components {

    public class InstantiaterMonoBehaviour: MonoBehaviour {
        
        protected GameObject Instantiate(GameObject original) {
            
            if (original == null) {
                
                return null;

            }

            return Instantiate<GameObject>(original);

        }

    }

}