namespace HGS.Tools.DI.Contexts {

    public interface IContext {

        public T Resolve<T>() where T: class;
        public void OnBind<T>();

    }

}
