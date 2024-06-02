using HGS.Asteroids.Tools;
using HGS.Tools.ECS.Components;
using UnityEngine;

namespace HGS.Asteroids.ECS.Components {

    public class SpaceshipMovementComponent: IComponent {

        public float FlightPower { get; private set; }
        
        public AnimationCurve FlightPowerAcceleration { get; private set; }
        public AnimationCurve FlightPowerDecay { get; private set; }

        public AnimationCurve AntiFlightPowerAcceleration { get; private set; }
        public AnimationCurve AntiFlightPowerDecay { get; private set; }

        public SpaceshipMovementComponent(float flightPower, AnimationCurve flightPowerAcceleration, AnimationCurve flightPowerDecay) {

            FlightPower = flightPower;

            FlightPowerAcceleration = flightPowerAcceleration;
            FlightPowerDecay = flightPowerDecay;

            AntiFlightPowerAcceleration = new AntiCurve(FlightPowerAcceleration).Curve;
            AntiFlightPowerDecay = new AntiCurve(FlightPowerDecay).Curve;

        }

        // промежуточное состояние
        public bool IsFlighting { get; set; }
        
        public AnimationCurve CurrenCurve { get; set; }

        public float TimeAction { get; set; }

        public Vector2 FlightDirection { get; set; }

    }

}