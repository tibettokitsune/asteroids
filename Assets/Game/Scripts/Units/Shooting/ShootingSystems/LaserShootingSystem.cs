using System.Collections.Generic;
using Game.Scripts.Infrastructure;
using Game.Scripts.UI;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using UnityEngine;

namespace Game.Scripts.Units.Shooting.ShootingSystems
{
    public class LaserShootingSystem : ShootingSystem
    {
        private float _timer;
        private int _availableNumberOfShoots;
        private const float RechargeTime = 10f;
        private const int MaximumNumberOfShoots = 2;
        private readonly GameplayHUDPanel _hud;
        
        public LaserShootingSystem(IPlayerInput input, IBulletsFactory bulletsFactory, 
            UnitPresenter source, GameplayHUDPanel hudPanel) : base(input, bulletsFactory, source)
        {
            _hud = hudPanel;
            _availableNumberOfShoots = MaximumNumberOfShoots;
            _timer = RechargeTime;
            _hud.UpdateNumberOfLaserShoots(_availableNumberOfShoots);
            
        }
        
        public override void UpdateItem()
        {
            base.UpdateItem();
            Recharge();

            if (PlayerInput.IsLaserShoot())
            {
                Shoot();
            }
            
            foreach (var bullet in Bullets)
            {
                bullet.UpdateItem();
            }
        }

        private void Recharge()
        {
            if (_availableNumberOfShoots < MaximumNumberOfShoots)
            {
                _timer -= Time.deltaTime;
                _timer = Mathf.Max(0f, _timer);
                if (_timer <= 0)
                {
                    IncrementAvailableShoots();
                    _timer += RechargeTime;
                }
            }
            
            _hud.UpdateLaserTimer(_timer);
        }

        private void IncrementAvailableShoots()
        {
            _availableNumberOfShoots++;
            _hud.UpdateNumberOfLaserShoots(_availableNumberOfShoots);
        }

        protected override void Shoot()
        {
            base.Shoot();
            if (_availableNumberOfShoots > 0)
            {
                Bullets.Add(BulletsFactory.SpawnLaserBullet(SourceUnit.Position, SourceUnit.Direction));
                _availableNumberOfShoots--;
                _hud.UpdateNumberOfLaserShoots(_availableNumberOfShoots);
            }
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