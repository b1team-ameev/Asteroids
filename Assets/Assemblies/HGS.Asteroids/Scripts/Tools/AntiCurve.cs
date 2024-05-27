using UnityEngine;

namespace HGS.Asteroids.Tools {

    public class AntiCurve {
        
        private readonly AnimationCurve antiCurve;

        public AnimationCurve Curve {

            get {

                return antiCurve;

            }

        }

        public AntiCurve(AnimationCurve curve) {

            if (curve == null) {
                
                return;

            }

            antiCurve = new AnimationCurve();

            // !!! работает некорректно
            for (int i = 0; i < curve.length; i++) {

                float inWeight = curve.keys[i].inTangent * curve.keys[i].inWeight;
                float outWeight = curve.keys[i].outTangent * curve.keys[i].outWeight;
                
                Keyframe inverseKey = new Keyframe(
                    curve.keys[i].value, curve.keys[i].time, 
                    Mathf.Atan(curve.keys[i].inTangent), Mathf.Atan(curve.keys[i].outTangent), 
                    inWeight, outWeight
                );
                
                antiCurve.AddKey(inverseKey);

            }
            
        }

        public float Evaluate(float value) {

            return antiCurve != null ? antiCurve.Evaluate(value) : default;

        }

    }

}