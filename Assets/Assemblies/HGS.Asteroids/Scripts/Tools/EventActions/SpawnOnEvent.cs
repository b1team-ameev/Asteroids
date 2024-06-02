using System;
using HGS.Asteroids.GameObjects;
using HGS.Asteroids.GameObjects.Spawners;
using HGS.Tools.Services.EventActions;
using UnityEngine;

namespace HGS.Asteroids.Tools.EventActions {

    [RequireComponent(typeof(ISpawner))]
    public class SpawnOnEvent: ActionOnEvent {
        
        protected override void OnEvent(EventArgs e) {

            ISpawner spawner = GetComponent<ISpawner>();

            if (spawner != null) {

                spawner.Spawn();

            }

        }
        
    }

}