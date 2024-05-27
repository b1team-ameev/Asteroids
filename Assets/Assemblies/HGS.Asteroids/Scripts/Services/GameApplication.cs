using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;
using HGS.Enums;

namespace HGS.Asteroids.Services {

    public class GameApplication: InjectedMonoBehaviour {
        
        [field:SerializeField]
        private GameObject GamePrefab { get; set; }

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();
            
            DontDestroyOnLoad(gameObject);

        }

        #endregion

        public void Init() {

            Debug.Log("GameApplication.Init");

            #if UNITY_EDITOR 

            PlayerPrefs.DeleteAll();

            #endif

            // инициализация игровых сервисов
            
            events?.Raise(EventKey.OnAppInitialized, true);

        }

        private void OnApplicationPause(bool pauseStatus) {
            
            events?.Raise(EventKey.OnAppPause, new UniversalEventArgs<bool>(pauseStatus));

        }

        public GameObject GetGameObject() {

            return GamePrefab != null ? Instantiate(GamePrefab) : default;

        }

        public void Exit() {

            Application.Quit();

        }

    }

}