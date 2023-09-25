using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.Units;
using Game.Scripts.Units.Behaviours;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        public Action OnGameOver { get; set; }
        
        private GameplayHUDPanel _hudPanel;
        
        private SpawnTimerSystem _timerSystem;
        
        private UnitsFactory _unitsFactory;
        private UnitPresenter _player;
        private readonly List<UnitPresenter> _systemsForUpdate = new List<UnitPresenter>();

        private void Start()
        {
            _unitsFactory = new UnitsFactory();
            _timerSystem = new SpawnTimerSystem(SpawnEnemy);
            _hudPanel = Instantiate(Resources.Load<GameplayHUDPanel>("GameplayHUD"));
            _player = _unitsFactory.CreatePlayer(new PlayerInput(), _hudPanel, () =>
            {
                //OnGameOver?.Invoke();
            });
            _systemsForUpdate.Add(_player);
            
        }

        private void SpawnEnemy()
        {
            var enemy = _unitsFactory.CreateEnemy(() =>
            {
                Debug.Log("Enemy collides");
            });
            _systemsForUpdate.Add(enemy);
        }

        private void Update()
        {
            _timerSystem.UpdateItem();

            foreach (var sys in _systemsForUpdate)
            {
                sys.UpdateItem();
            }
            
            CheckLevelFail();
        }

        private void CheckLevelFail()
        {
            var isCollide = CollisionHelper.CalculateCollisionForTarget(_player, _systemsForUpdate);
            if (!isCollide) return;
            _player.OnCollide.Invoke();
        }
    }
}