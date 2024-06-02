using System;
using HGS.Asteroids.GameObjects;
using UnityEngine;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Services.EventActions;
using HGS.Asteroids.GameObjects.Spawners;

namespace HGS.Asteroids.Tools.EventActions {

    [RequireComponent(typeof(ISpawner))]
    public class SpawnByNewWaveOnEvent: ActionOnEvent {
        
        [field:SerializeField]
        public int WaveNumber { get; private set; }

        protected override void OnEvent(EventArgs e) {

            var pE = e as UniversalEventArgs<int>;

            if (pE != null && pE.Value >= WaveNumber) {

                ISpawner spawner = GetComponent<ISpawner>();

                if (spawner != null) {

                    spawner.Spawn();

                }

            }

        }
        
    }

}