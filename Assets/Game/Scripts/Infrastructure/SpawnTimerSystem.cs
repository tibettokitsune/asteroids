using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Infrastructure
{
    public class SpawnTimerSystem : IUpdateItem
    {
        public readonly Action OnSpawnTimerTriggered;
        private float _timer;
        private float _currentTime;
        private readonly Vector2 _spawnBounds = new Vector2(1, 2);

        public SpawnTimerSystem(Action onSpawnTimerTriggered)
        {
            OnSpawnTimerTriggered = onSpawnTimerTriggered;
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
            OnSpawnTimerTriggered.Invoke();
            _currentTime = Random.Range(_spawnBounds.x, _spawnBounds.y);
            _timer += _currentTime;
        }
    }
}