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
        public Action<UnitPresenter> OnUnitSpawn { get; set; }
        private ViewsConfiguration _viewsConfiguration;

        private Camera _camera;

        private PlayerPresenter _player;
        public UnitsFactory()
        {
            _viewsConfiguration = Resources.Load<ViewsConfiguration>("UnitsViewAsset");
            _camera = Camera.main;
        }
        public UnitPresenter CreatePlayer(IPlayerInput playerInput, GameplayHUDPanel hud, Action<Vector2> collideAction)
        {
            _player = new PlayerPresenter(_viewsConfiguration.playerConfiguration, Vector2.zero, collideAction, playerInput, hud);
            return _player;
        }

        public UnitPresenter CreateEnemy(Action<Vector2> collideAction)
        {
            var seed = Random.Range(0, 100);
            var position = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), _camera.transform.position.z);
            var worldPosition = _camera.ScreenToWorldPoint(position);
            if (seed > 50)
            {
                var asteroid = new AsteroidPresenter(_viewsConfiguration.asteroidConfiguration, worldPosition,
                    pos =>
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            CreateLittleAsteroid(pos, collideAction);
                        }
                        
                        collideAction.Invoke(pos);
                    });
                OnUnitSpawn.Invoke(asteroid);
                return asteroid;
            }
            else
            {
                var alien = new AlienPresenter(_viewsConfiguration.alienConfiguration, worldPosition, collideAction, _player);
                OnUnitSpawn.Invoke(alien);
                return alien;
            }
        }

        private UnitPresenter CreateLittleAsteroid(Vector2 worldPosition, Action<Vector2> collideAction)
        {
            var asteroid = new AsteroidPresenter(_viewsConfiguration.asteroidConfiguration, 
                worldPosition + Random.insideUnitCircle, collideAction);
            asteroid.ScaleUnit(Random.Range(0.3f, 0.6f));
            asteroid.UpdateAsteroidSpeed(10f);
            OnUnitSpawn.Invoke(asteroid);
            return asteroid;
        }
    }
}