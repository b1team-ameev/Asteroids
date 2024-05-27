using HGS.Asteroids.GameObjects;
using HGS.Asteroids.Services;
using HGS.Tools.DI;
using UnityEngine;

namespace HGS.Asteroids.Binders {

    public class SceneGameBinder: Binder {

        [field:SerializeField]
        private GameObject InputValuesPlayerPrefab { get; set; }
        [field:SerializeField]
        private GameObject InputValuesUIPrefab { get; set; }
        [field:SerializeField]
        private GameObject SpaceshipPrefab { get; set; }

        public override void Bind() {

            Debug.Log("SceneGameBinder.Bind");
            
            BindInputValues();
            BindSpaceship();

        }

        private void BindSpaceship() {
            
            // выриант генерации через SpawnerSpaceship
            Container.BindFromPrefab<Spaceship>(SpaceshipPrefab);
            Container.BindAsSingle<Spaceship>(false);

        }

        private void BindInputValues() {
            
            Container.BindFromPrefab<InputValuesPlayer>(InputValuesPlayerPrefab);
            Container.BindAsSingle<InputValuesPlayer>();

            Container.BindFromPrefab<InputValuesUI>(InputValuesUIPrefab);
            Container.BindAsSingle<InputValuesUI>();

        }
        
    }

}