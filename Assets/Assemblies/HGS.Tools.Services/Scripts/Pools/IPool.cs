using HGS.Enums;

namespace HGS.Tools.Services.Pools {

    public interface IPool {

        public object Get();
        public void Release(object releasedObject);

        public void Clear();

    }

}