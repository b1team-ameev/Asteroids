using HGS.Asteroids.Enums;
using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components.Damagers {

    public interface IDamagerComponent: IComponent {
        
        public Damage Damage { get; }

    }

}