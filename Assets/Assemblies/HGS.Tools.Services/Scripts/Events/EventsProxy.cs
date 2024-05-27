using HGS.Tools.DI.Injection;

namespace HGS.Tools.Services.ServiceEvents {

    public class EventsProxy {

        public Events Events { get; private set; }

        [Inject]
        private void Constructor(Events events) {
            
            Events = events;

        }

        public EventsProxy() {

            Injector.Inject(this);

        }

    }

}