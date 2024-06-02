using HGS.Asteroids.ECS.Components;
using HGS.Asteroids.Services;
using HGS.Tools.ECS.EntityFilters;
using HGS.Tools.ECS.Systems;
using UnityEngine;

namespace HGS.Asteroids.ECS.Systems {

    public class SpaceshipRotatingSystem: ISystem {

        [EntityFilter(typeof(EntityFilter<SpaceshipRotatingComponent, InputValuesPlayerComponent, TransformBaseComponent>))]
        public IEntityFilter EntityFilter { get; private set; }

        public void Run() {

            foreach(var entity in EntityFilter.Entities) {

                if (entity != null) {

                    SpaceshipRotatingComponent spaceshipRotatingComponent = entity.Get<SpaceshipRotatingComponent>(0);
                    InputValuesPlayerComponent inputValuesPlayerComponent = entity.Get<InputValuesPlayerComponent>(1); 
                    TransformBaseComponent transformComponent = entity.Get<TransformBaseComponent>(2);

                    if (spaceshipRotatingComponent != null && inputValuesPlayerComponent != null && transformComponent != null) {

                        InputValuesPlayer inputValues = inputValuesPlayerComponent.InputValuesPlayer;
                        Vector2 move = inputValues != null ? inputValues.MoveRaw : default;

                        if (move.x != 0f) {

                            float rotateDirection = -move.x;

                            transformComponent.Transform.Rotate(0f, 0f, spaceshipRotatingComponent.RotateAngle * rotateDirection * Time.deltaTime);

                        }

                    }

                }

            }
            
        }

    }

}