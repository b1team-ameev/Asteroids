namespace HGS.Tools.DI.Injection {

    public abstract class InjectedObject {

        protected InjectedObject() {

            Injector.Inject(this);

        }

    }

}