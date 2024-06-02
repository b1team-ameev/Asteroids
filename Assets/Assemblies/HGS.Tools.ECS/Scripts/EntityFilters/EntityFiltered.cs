using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.Entities;

namespace HGS.Tools.ECS.EntityFilters {

    public class EntityFiltered {
        
        public IEntity Entity { get; private set; }

        private IComponent[] components;

        public EntityFiltered(IEntity entity, int count) {

            Entity = entity;
            components = new IComponent[count];

        }

        public void Set(int index, IComponent component) {

            if (components != null && index < components.Length) {

                components[index] = component;

            }

        }

        public IComponent Get(int index) {

            if (components != null && index < components.Length) {

                return components[index];

            }

            return null;

        }

        public T Get<T>(int index) where T:IComponent {

            return (T)Get(index);

        }

        public void Destroy() {

            components = null;
            Entity = null;

        }

    }

}
