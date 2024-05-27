using HGS.Asteroids.GameObjects.Weapons;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.DI.Injection;
using UnityEngine;
using HGS.Enums;

namespace HGS.Asteroids.GameObjects.Informers {

    [RequireComponent(typeof(IReloadableWeapon))]
    public class InformerTimeBeforeReload: InjectedMonoBehaviour {
        
        private IReloadableWeapon reloadableWeapon;

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();

            reloadableWeapon = GetComponent<IReloadableWeapon>();

        }
        
        private void Update() {

            ShowData();

        }
        
        private void OnDestroy() {

            Show();

        }

        #endregion

        private void ShowData() {
            
            if (reloadableWeapon != null) {

                Show(reloadableWeapon.TimeBeforeReload);

            }
            
        }

        private void Show(float value = default) {

            events?.Raise(EventKey.OnShowTimeBeforeReload, new UniversalEventArgs<float>(value));

        }

    }

}