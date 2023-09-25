using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.UI;
using Game.Scripts.Units.Behaviours;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Units
{
    public class UnitsFactory
    {
        private UnitsConfiguration _unitsConfiguration;

        private Camera _camera;

        private PlayerPresenter _player;
        public UnitsFactory()
        {
            _unitsConfiguration = Resources.Load<UnitsConfiguration>("UnitsViewAsset");
            _camera = Camera.main;
        }
        public UnitPresenter CreatePlayer(IPlayerInput playerInput, GameplayHUDPanel hud, Action collideAction)
        {
            _player = new PlayerPresenter(_unitsConfiguration.playerConfiguration, Vector2.zero, collideAction, playerInput, hud);
            return _player;
        }

        public UnitPresenter CreateEnemy(Action collideAction)
        {
            var seed = Random.Range(0, 100);
            var position = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), _camera.transform.position.z);
            var worldPosition = _camera.ScreenToWorldPoint(position);
            if (seed > 50)
            {
                var asteroid = new AsteroidPresenter(_unitsConfiguration.asteroidConfiguration, worldPosition, collideAction);

                return asteroid;
            }
            else
            {
                var alien = new AlienPresenter(_unitsConfiguration.alienConfiguration, worldPosition, collideAction, _player);

                return alien;
            }
        }
    }
}