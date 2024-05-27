using System;

namespace HGS.Tools.DI.Factories {

    public abstract class Factory<T>: IFactory<T> {

        public abstract object Create();

        public Type GetResultType() {
            
            return typeof(T);

        }
        
    }

}
