using System.Collections.Generic;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using UnityEngine;

namespace Game.Scripts.Units.Shooting.ShootingSystems
{
    public class LaserShootingSystem : ShootingSystem
    {
        
        private float _timer;
        private int _availableShootNumber;
        private const float ShootingCooldown = 1f;
        public LaserShootingSystem(IPlayerInput input, IBulletsFactory bulletsFactory, UnitPresenter source) : base(input, bulletsFactory, source)
        {
        }
        
        public override void UpdateItem()
        {
            base.UpdateItem();
            _timer -= Time.deltaTime;
            _timer = Mathf.Max(0f, _timer);
            if (PlayerInput.IsLaserShoot())
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
            Bullets.Add(BulletsFactory.SpawnLaserBullet(SourceUnit.Position, SourceUnit.Direction));
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

                bullet.GetDamage();
                return targets;
            }

            return null;
        }
    }
}