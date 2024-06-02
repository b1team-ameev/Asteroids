using HGS.Tools.ECS.Components;

namespace HGS.Asteroids.ECS.Components {

    public interface IPointsComponent: IComponent {

        public int Value { get; }

    }

}