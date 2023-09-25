using System.Collections.Generic;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using UnityEngine;

namespace Game.Scripts.Units.Shooting
{
    public class SimpleShootingSystem : IUpdateItem
    {
        private IPlayerInput _playerInput;
        private BulletsFactory _bulletsFactory;
        private UnitPresenter _sourceUnit;
        
        private const float ShootingCooldown = 0.4f;

        private float _timer;

        private List<BulletPresenter> _bullets = new List<BulletPresenter>();

        public SimpleShootingSystem(IPlayerInput input, BulletsFactory bulletsFactory, UnitPresenter source)
        {
            _playerInput = input;
            _bulletsFactory = bulletsFactory;
            _sourceUnit = source;
        }
        public void UpdateItem()
        {
            
            _timer -= Time.deltaTime;
            _timer = Mathf.Max(0f, _timer);
            if (_playerInput.IsSimpleShoot())
            {

                if (_timer <= 0)
                {
                    Shoot();
                    _timer += ShootingCooldown;
                }
            }

            foreach (var bullet in _bullets)
            {
                bullet.UpdateItem();
            }
        }

        private void Shoot()
        {
            _bullets.Add(_bulletsFactory.SpawnDefaultBullet(_sourceUnit.Position, _sourceUnit.Direction));
            
        }

        public List<UnitPresenter> ComputeCollision(List<UnitPresenter> units)
        {
            foreach (var bullet in _bullets)
            {
                var isCollide = CollisionHelper.CalculateCollisionForTarget(bullet, units);
                if (isCollide)
                {
                    var targets = CollisionHelper.CollidedUnits(bullet, units);

                    foreach (var t in targets)
                    {
                        t.GetDamage();
                    }

                    _bullets.Remove(bullet);
                    bullet.GetDamage();
                    return targets;
                }
            }

            return null;
        }
    }
}