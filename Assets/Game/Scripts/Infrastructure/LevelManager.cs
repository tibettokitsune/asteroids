using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.Units;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using Game.Scripts.Units.Shooting;
using Game.Scripts.Units.Shooting.ShootingSystems;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        public Action<int> OnGameOver { get; set; }
        private GameplayHUDPanel _hudPanel;
        private ScoreController _scoreController;
        private SpawnTimerSystem _timerSystem;
        private UnitsFactory _unitsFactory;
        private UnitPresenter _player;
        private readonly List<UnitPresenter> _units = new List<UnitPresenter>();
        private BulletsFactory _bulletsFactory;
        private ShootingSystem _commonShootingSystem;
        private ShootingSystem _laserShootingSystem;
        private void Start()
        {
            _unitsFactory = new UnitsFactory();
            _unitsFactory.OnUnitSpawn += unit =>
            {
                _units.Add(unit);
            };
            _timerSystem = new SpawnTimerSystem(SpawnEnemy);
            _hudPanel = Instantiate(Resources.Load<GameplayHUDPanel>("GameplayHUD"));
            _scoreController = new ScoreController(_hudPanel);
            
            var playerInput = new PlayerInput();
            _player = _unitsFactory.CreatePlayer(playerInput, _hudPanel, _ =>
            {
                OnGameOver?.Invoke(_scoreController.Score);
            });
            _units.Add(_player);

            _bulletsFactory = new BulletsFactory();
            _commonShootingSystem = new CommonShootingSystem(playerInput, _bulletsFactory, _player);
            _laserShootingSystem = new LaserShootingSystem(playerInput, _bulletsFactory, _player, _hudPanel);
        }
        
        

        private void SpawnEnemy()
        {
            _unitsFactory.CreateEnemy(_ =>
            {
                _scoreController.IncrementScore();
            });
            
        }

        private void Update()
        {
            _timerSystem.UpdateItem();
            
            WeaponCompute();
            
            foreach (var sys in _units)
            {
                sys.UpdateItem();
            }
            
            CheckLevelFail();
        }

        private void WeaponCompute()
        {
            ComputeCommonWeapon();
            ComputeLaser();
        }

        private void ComputeLaser()
        {
            _laserShootingSystem.UpdateItem();
            BulletCollisionsCheck(_laserShootingSystem);
        }

        private void ComputeCommonWeapon()
        {
            _commonShootingSystem.UpdateItem();
            BulletCollisionsCheck(_commonShootingSystem);
        }

        private void BulletCollisionsCheck(ShootingSystem shootingSystem)
        {
            var damaged = shootingSystem.ComputeCollision(_units);

            if (damaged == null) return;
            foreach (var d in damaged)
            {
                _units.Remove(d);
            }
        }

        private void CheckLevelFail()
        {
            var isCollide = CollisionHelper.CalculateCollisionForTarget(_player, _units);
            if (!isCollide) return;
            _player.OnCollide.Invoke(_player.Position);
        }
    }
}