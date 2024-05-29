using HGS.Asteroids.GameObjects.Weapons;
using HGS.Tools.Services.ServiceEvents;
using HGS.Enums;

namespace HGS.Asteroids.GameObjects.Informers {

    public class InformerTimeBeforeReload: IInformer {
        
        private readonly Events events;
        private readonly IReloadableWeapon reloadableWeapon;

        public InformerTimeBeforeReload(Events events, IReloadableWeapon reloadableWeapon) {
            
            this.events = events;
            this.reloadableWeapon = reloadableWeapon;

        }

        public void ShowInfo() {
            
            if (reloadableWeapon != null) {

                Show(reloadableWeapon.TimeBeforeReload);

            }
            
        }
        
        public void Clear() {
            
            Show();
            
        }

        private void Show(float value = default) {

            events?.Raise(EventKey.OnShowTimeBeforeReload, new UniversalEventArgs<float>(value));

        }

    }

}