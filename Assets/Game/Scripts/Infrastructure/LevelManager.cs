using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        public Action OnGameOver { get; set; }
        private readonly List<UnitPresenter> _systemsForUpdate = new List<UnitPresenter>();
        private UnitsFactory _unitsFactory;
        private GameplayHUDPanel _hudPanel;
        private SpawnTimerSystem _timerSystem;
        private void Start()
        {
            _unitsFactory = new UnitsFactory();
            _timerSystem = new SpawnTimerSystem(SpawnEnemy);
            _hudPanel = Instantiate(Resources.Load<GameplayHUDPanel>("GameplayHUD"));
            var player = _unitsFactory.CreatePlayer(new PlayerInput(), _hudPanel, () =>
            {
                Debug.Log("Player collides");
            });
            _systemsForUpdate.Add(player);
            
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
                
                var isCollide = CollisionHelper.CalculateCollisionForTarget(sys, _systemsForUpdate);
                if (isCollide)
                {
                    sys.OnCollide.Invoke();
                    OnGameOver?.Invoke();
                }
            }
        }
    }
}