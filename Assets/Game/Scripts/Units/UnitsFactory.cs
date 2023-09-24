using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Units
{
    public class UnitsFactory
    {
        private UnitsConfiguration _unitsConfiguration;

        private Camera _camera;
        public UnitsFactory()
        {
            _unitsConfiguration = Resources.Load<UnitsConfiguration>("UnitsViewAsset");
            _camera = Camera.main;
        }
        public IUpdateItem CreatePlayer(IPlayerInput playerInput, GameplayHUDPanel hud)
        {
            var player = new PlayerPresenter(_unitsConfiguration.playerConfiguration, Vector2.zero, playerInput, hud);

            return player;
        }

        public IUpdateItem CreateEnemy()
        {
            var seed = Random.Range(0, 100);
            var position = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), _camera.transform.position.z);
            var worldPosition = _camera.ScreenToWorldPoint(position);
            if (seed > 50)
            {
                var asteroid = new AsteroidPresenter(_unitsConfiguration.asteroidConfiguration, worldPosition);

                return asteroid;
            }
            else
            {
                var asteroid = new AsteroidPresenter(_unitsConfiguration.asteroidConfiguration, worldPosition);

                return asteroid;
            }
        }
    }
}