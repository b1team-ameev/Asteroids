using HGS.Asteroids.GameObjects.Weapons;
using HGS.Tools.Services.ServiceEvents;
using HGS.Enums;

namespace HGS.Asteroids.GameObjects.Informers {

    public class InformerBulletCount: IInformer {
        
        private readonly Events events;
        private readonly ILimitedSupplyWeapon limitedSupplyWeapon;
        
        public InformerBulletCount(Events events, ILimitedSupplyWeapon limitedSupplyWeapon) {
            
            this.events = events;
            this.limitedSupplyWeapon = limitedSupplyWeapon;

        }

        public void ShowInfo() {
            
            if (limitedSupplyWeapon != null) {

                Show(limitedSupplyWeapon.BulletCount);

            }
            
        }
        
        public void Clear() {
            
            Show();
            
        }

        private void Show(int value = default) {

            events?.Raise(EventKey.OnShowBulletCount, new UniversalEventArgs<int>(value));

        }

    }

}