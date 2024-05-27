using System;

namespace HGS.Tools.DI.Factories {

    public interface IFactory {
        
        public Type GetResultType();
        public object Create();

    }

}
