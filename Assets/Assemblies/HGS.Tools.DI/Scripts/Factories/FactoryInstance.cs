namespace HGS.Tools.DI.Factories {

    public class FactoryInstance<T>: Factory<T> {

        private readonly T instance;

        public FactoryInstance(T instance) {

            this.instance = instance;

        }

        public override object Create() {

            return instance;

        }

    }

}
