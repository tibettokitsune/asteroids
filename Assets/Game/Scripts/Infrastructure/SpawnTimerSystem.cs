using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Infrastructure
{
    public class SpawnTimerSystem : IUpdateItem
    {
        private readonly Action _onSpawnTimerTriggered;
        private float _timer;
        private float _currentTime;
        private readonly Vector2 _spawnBounds = new Vector2(3, 6);

        public SpawnTimerSystem(Action onSpawnTimerTriggered)
        {
            _onSpawnTimerTriggered = onSpawnTimerTriggered;
        }

        public void UpdateItem()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            _onSpawnTimerTriggered.Invoke();
            _currentTime = Random.Range(_spawnBounds.x, _spawnBounds.y);
            _timer += _currentTime;
        }
    }
}