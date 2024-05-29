using HGS.Enums;
using HGS.Tools.DI.Injection;
using HGS.Tools.Services.Pools;
using UnityEngine;

namespace HGS.Asteroids.GameObjects {

    public class Explosion: InjectedMonoBehaviour, IExplosion {
        
        [field:SerializeField]
        private ObjectVariant ExplosionVariant { get; set; }

        private IPoolStack poolStack;

        [Inject]
        private void Constructor(IPoolStack poolStack) {
            
            this.poolStack = poolStack;

        }

        public void Exploud() {

            Transform thisTransform = transform;

            GameObject explosion = poolStack?.Get(ExplosionVariant) as GameObject;

            if (explosion != null && thisTransform != null) {

                Transform explosionTransform = explosion.transform;

                if (explosionTransform != null) {

                    explosionTransform.parent = thisTransform.parent;

                    explosionTransform.position = thisTransform.position;

                }

            }

        }

    }

}