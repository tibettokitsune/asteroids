using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.Units;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using Game.Scripts.Units.Shooting;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        public Action OnGameOver { get; set; }
        
        private GameplayHUDPanel _hudPanel;

        private ScoreController _scoreController;
        
        private SpawnTimerSystem _timerSystem;
        
        private UnitsFactory _unitsFactory;
        private UnitPresenter _player;
        private readonly List<UnitPresenter> _units = new List<UnitPresenter>();

        private BulletsFactory _bulletsFactory;
        private SimpleShootingSystem _simpleShootingSystem;
        private void Start()
        {
            _unitsFactory = new UnitsFactory();
            _timerSystem = new SpawnTimerSystem(SpawnEnemy);
            _hudPanel = Instantiate(Resources.Load<GameplayHUDPanel>("GameplayHUD"));
            _scoreController = new ScoreController(_hudPanel);
            
            var playerInput = new PlayerInput();
            _player = _unitsFactory.CreatePlayer(playerInput, _hudPanel, () =>
            {
                OnGameOver?.Invoke();
            });
            _units.Add(_player);

            _bulletsFactory = new BulletsFactory();
            _simpleShootingSystem = new SimpleShootingSystem(playerInput, _bulletsFactory, _player);
            
        }

        private void SpawnEnemy()
        {
            var enemy = _unitsFactory.CreateEnemy(() =>
            {
                _scoreController.IncrementScore();
            });
            _units.Add(enemy);
        }

        private void Update()
        {
            _timerSystem.UpdateItem();
            _simpleShootingSystem.UpdateItem();
            BulletCollisionsCheck();
            foreach (var sys in _units)
            {
                sys.UpdateItem();
            }
            
            CheckLevelFail();
        }

        private void BulletCollisionsCheck()
        {
            var damaged = _simpleShootingSystem.ComputeCollision(_units);

            if (damaged != null)
            {
                foreach (var d in damaged)
                {
                    _units.Remove(d);
                }
            }
        }

        private void CheckLevelFail()
        {
            var isCollide = CollisionHelper.CalculateCollisionForTarget(_player, _units);
            if (!isCollide) return;
            _player.OnCollide.Invoke();
        }
    }
}