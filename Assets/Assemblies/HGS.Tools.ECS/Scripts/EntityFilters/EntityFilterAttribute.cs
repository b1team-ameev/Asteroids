using System;
using System.Linq;

namespace HGS.Tools.ECS.EntityFilters {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EntityFilterAttribute: Attribute {
                
        public Type FilterType { get; private set; }

        public EntityFilterAttribute(Type filterType) {

            if (filterType == null || !filterType.GetInterfaces().Contains(typeof(IEntityFilter))) {

                throw new ArgumentException();

            }

            FilterType = filterType;

        }

    }

}
