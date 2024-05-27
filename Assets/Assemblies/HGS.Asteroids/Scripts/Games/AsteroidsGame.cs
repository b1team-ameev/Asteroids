using UnityEngine;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;

namespace HGS.Asteroids.Games {

    public class AsteroidsGame: Game {
        
        #region Игровые данные

        [field:SerializeField]
        public int Score { get; private set; } 
        [field:SerializeField]
        public int WaveNumber { get; private set; }
        
        [field:SerializeField]
        public float TimeInWaveForStartUfo { get; private set; }

        #endregion

        #region Игровой процесс

        public void NewWave() {

            WaveNumber++;

            events?.Raise(EventKey.OnNewWave, new UniversalEventArgs<int>(WaveNumber));

        }

        public void AddPoints(int value) {
            
            Score += value;

            events?.Raise(EventKey.OnShowScore, new UniversalEventArgs<int>(Score));

        }

        public void SpawnUfo() {

            events?.Raise(EventKey.OnNeedSpawnUfo);

        }

        #endregion

    }

}