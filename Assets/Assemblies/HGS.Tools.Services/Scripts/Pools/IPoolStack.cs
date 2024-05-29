using HGS.Enums;

namespace HGS.Tools.Services.Pools {

    public interface IPoolStack {

        public object Get(ObjectVariant objectVariant);
        public void Release(object releasedObject);
        
        public void RegisterPool(ObjectVariant objectVariant, IPool pool);

    }

}