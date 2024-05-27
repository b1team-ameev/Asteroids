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

            if (curve == null || curve.length < 2) {
                
                return;

            }

            antiCurve = new AnimationCurve();

            float start = curve[0].time;
            float end = curve[curve.length - 1].time; 

            float delta = (end - start) * 0.01f;

            for(; start <= end; start += delta) {

                antiCurve.AddKey(new Keyframe(curve.Evaluate(start), start));

            }

        }

        public float Evaluate(float value) {

            return antiCurve != null ? antiCurve.Evaluate(value) : default;

        }

    }

}