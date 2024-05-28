using HGS.Asteroids.Services;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.ServiceSounds;
using HGS.Tools.Services.Collections;
using HGS.Tools.DI;
using UnityEngine;

namespace HGS.Asteroids.Binders {

    public class Bootstrap: Binder {

        [field:SerializeField]
        private GameObject EventsPrefab { get; set; }
        [field:SerializeField]
        private GameObject SoundsPrefab { get; set; }
        [field:SerializeField]
        private GameObject GameApplicationPrefab { get; set; }

        public override void Bind() {

            Debug.Log("Bootstrap.Bind");
            
            BindEvents();
            BindSounds();
            
            BindApp();

        }

        private void BindApp() {
            
            Container.BindFromPrefab<GameApplication>(GameApplicationPrefab).AsSingle();

        }

        private void BindSounds() {
            
            Container.BindTo<ICollection<AudioClip>, SoundCollection>();

            Container.BindFromPrefab<Sounds>(SoundsPrefab).AsSingle();

        }

        private void BindEvents() {
            
            Container.BindFromPrefab<Events>(EventsPrefab).AsSingle();

        }
    }

}