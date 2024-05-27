using HGS.Asteroids.GameObjects.Weapons;
using HGS.Tools.Services.ServiceEvents;
using HGS.Enums;
using HGS.Tools.DI.Injection;
using UnityEngine;

namespace HGS.Asteroids.GameObjects.Informers {

    [RequireComponent(typeof(ILimitedSupplyWeapon))]
    public class InformerBulletCount: InjectedMonoBehaviour {
        
        private ILimitedSupplyWeapon limitedSupplyWeapon;

        private Events events;

        [Inject]
        private void Constructor(Events events) {
            
            this.events = events;

        }

        #region Awake/Start/Update/FixedUpdate

        private new void Awake() {

            base.Awake();

            limitedSupplyWeapon = GetComponent<ILimitedSupplyWeapon>();

        }
        
        private void Update() {

            ShowData();

        }
        
        private void OnDestroy() {

            Show();

        }

        #endregion

        private void ShowData() {
            
            if (limitedSupplyWeapon != null) {

                Show(limitedSupplyWeapon.BulletCount);

            }
            
        }

        private void Show(int value = default) {

            events?.Raise(EventKey.OnShowBulletCount, new UniversalEventArgs<int>(value));

        }

    }

}