using HGS.Tools.ECS.Components;

namespace HGS.Tools.ECS.Entities {

    public interface IEntity {
        
        public object IdObject { get; } 
        public void SetIdObject(object idObject);

        public T AddComponent<T>() where T: class, IComponent;
        public T AddComponent<T>(T component) where T: class, IComponent;

        public T GetComponent<T>() where T: IComponent;

    }

}
