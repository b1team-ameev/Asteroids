namespace HGS.Asteroids.ECS.Components {

    public class PointsComponent: IPointsComponent {
        
        public int Value { get; private set; }

        public PointsComponent(int value) {

            Value = value;

        }

    }

}