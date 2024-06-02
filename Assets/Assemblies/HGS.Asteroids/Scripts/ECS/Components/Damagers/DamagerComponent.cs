using HGS.Asteroids.Enums;

namespace HGS.Asteroids.ECS.Components.Damagers {

    public class DamagerComponent: IDamagerComponent {

        public Damage Damage { get; set; }

        public DamagerComponent(Damage damage) {

            Damage = damage;

        }

    }

}