using System;
using System.Collections.Generic;
using Game.Scripts.UI;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class LevelManager : MonoBehaviour
    {
        private List<IUpdateItem> _systemsForUpdate = new List<IUpdateItem>();
        private UnitsFactory _unitsFactory;
        private GameplayHUDPanel _hudPanel;
        private void Start()
        {
            _unitsFactory = new UnitsFactory();
            var timerSystem = new SpawnTimerSystem(SpawnEnemy);
            _systemsForUpdate.Add(timerSystem);
            _hudPanel = Instantiate(Resources.Load<GameplayHUDPanel>("GameplayHUD"));
            var player = _unitsFactory.CreatePlayer(new PlayerInput(), _hudPanel);
            _systemsForUpdate.Add(player);
            
        }

        private void SpawnEnemy()
        {
            // var enemy = _unitsFactory.CreateEnemy();
            // _systemsForUpdate.Add(enemy);
        }

        public void UpdateItem()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            foreach (var sys in _systemsForUpdate)
            {
                sys.UpdateItem();
            }
        }
    }
}