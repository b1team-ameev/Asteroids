namespace HGS.Tools.DI.Factories {

    public class FactorySingletone<T>: Factory<T> {

        private readonly IFactory<T> factory;
        private T instance;

        public FactorySingletone(IFactory<T> factory) {

            this.factory = factory;

        }

        public override object Create() {

            if (instance == null) {

                instance = (T)factory.Create();

            }

            return instance;

        }

    }

}