namespace HGS.Tools.DI.Factories {

    public class FactoryConstructor<T>: Factory<T> where T: new() {

        public override object Create() {

            return new T();

        }

    }

}
