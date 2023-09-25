using System.Collections.Generic;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using UnityEngine;

namespace Game.Scripts.Units.Shooting.ShootingSystems
{
    public class CommonShootingSystem : ShootingSystem
    {
        private const float ShootingCooldown = 0.4f;
        private float _timer;
        
        public override void UpdateItem()
        {
            base.UpdateItem();
            _timer -= Time.deltaTime;
            _timer = Mathf.Max(0f, _timer);
            if (PlayerInput.IsSimpleShoot())
            {

                if (_timer <= 0)
                {
                    Shoot();
                    _timer += ShootingCooldown;
                }
            }

            foreach (var bullet in Bullets)
            {
                bullet.UpdateItem();
            }
        }

        protected override void Shoot()
        {
            base.Shoot();
            Bullets.Add(BulletsFactory.SpawnDefaultBullet(SourceUnit.Position, SourceUnit.Direction));
        }

        public override List<UnitPresenter> ComputeCollision(List<UnitPresenter> units)
        {
            foreach (var bullet in Bullets)
            {
                var isCollide = CollisionHelper.CalculateCollisionForTarget(bullet, units);
                if (!isCollide) continue;
                var targets = CollisionHelper.CollidedUnits(bullet, units);

                foreach (var t in targets)
                {
                    t.GetDamage();
                }

                Bullets.Remove(bullet);
                bullet.GetDamage();
                return targets;
            }

            return null;
        }

        public CommonShootingSystem(IPlayerInput input, IBulletsFactory bulletsFactory, UnitPresenter source) : base(input, bulletsFactory, source)
        {
        }
    }
}