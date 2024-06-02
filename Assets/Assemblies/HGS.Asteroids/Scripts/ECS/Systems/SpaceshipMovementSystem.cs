using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.Services;
using HGS.Tools.ECS.Components;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceshipMovementSystem: ISystem {

        [EntityFilter(typeof(EntityFilter<SpaceshipMovementComponent, InputValuesPlayerComponent, TransformComponent, TransformBaseComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    SpaceshipMovementComponent spaceshipMovementComponent = entity.Get<SpaceshipMovementComponent>(0);
                    InputValuesPlayerComponent inputValuesPlayerComponent = entity.Get<InputValuesPlayerComponent>(1); 
                    TransformComponent transformComponent = entity.Get<TransformComponent>(2);
                    TransformBaseComponent baseTransformComponent = entity.Get<TransformBaseComponent>(3);

                    if (inputValuesPlayerComponent != null) {

                        InputValuesPlayer inputValues = inputValuesPlayerComponent.InputValuesPlayer;
                        Vector2 move = inputValues != null ? inputValues.MoveRaw : default;

                        SetFlighting(spaceshipMovementComponent, baseTransformComponent, move.y == 1f);
                        Fly(spaceshipMovementComponent, transformComponent);

                    }

                }

            }
            
        }

        private void SetFlighting(SpaceshipMovementComponent spaceship, TransformBaseComponent baseTransform, bool isFlighting) {

            if (spaceship == null) {

                return;

            }

            if (spaceship.IsFlighting != isFlighting) {

                spaceship.IsFlighting = isFlighting;

                // по текущему Value пытаемся найти Time на противоположной Cure с таким же Value для плавного ускорения-замедления
                float currentValue = spaceship.CurrenCurve != null ? spaceship.CurrenCurve.Evaluate(spaceship.TimeAction) : default;
                spaceship.CurrenCurve = isFlighting ? spaceship.FlightPowerAcceleration : spaceship.FlightPowerDecay;

                if (currentValue > 0f && spaceship.TimeAction > 0f) {

                    AnimationCurve antiCurve = isFlighting ? spaceship.AntiFlightPowerAcceleration : spaceship.AntiFlightPowerDecay;

                    if (antiCurve != null) {

                        // float t2 = antiCurve.Evaluate(currentValue);
                        // float v2 = spaceship.CurrenCurve.Evaluate(t2);
                        
                        // Debug.Log($"t1 {spaceship.TimeAction}; v1 {currentValue}; t2 {t2}; v2 {v2}");

                        spaceship.TimeAction = antiCurve.Evaluate(currentValue);

                    }

                }
                else {

                    spaceship.TimeAction = 0f;

                }

                // направление полета
                if (isFlighting && baseTransform != null) {

                    spaceship.FlightDirection = baseTransform.Transform.rotation * Vector2.up;

                }

            }
            
        }

        private void Fly(SpaceshipMovementComponent spaceship, TransformComponent transform) {
            
            if (spaceship == null || spaceship.CurrenCurve == null || transform == null) {

                return;

            }

            spaceship.TimeAction += Time.deltaTime;

            float actionPower = spaceship.CurrenCurve.Evaluate(spaceship.TimeAction);

            if (actionPower == 0f) {

                return;

            }

            transform.Transform.Translate(spaceship.FlightDirection * actionPower * spaceship.FlightPower * Time.deltaTime);

        }

    }

}