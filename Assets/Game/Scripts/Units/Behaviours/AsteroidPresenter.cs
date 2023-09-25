using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Units.Behaviours
{
    public class AsteroidPresenter : UnitPresenter
    {
        private readonly Camera _camera;
        private readonly Vector2 _direction;
        private readonly SimplePhysicsData _physicsData;
        
        public AsteroidPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, Action onCollide) 
            : base(unitConfiguration, spawnPosition, onCollide)
        {
            _camera = Camera.main;
            _direction = Random.insideUnitCircle;
            _physicsData = new SimplePhysicsData() {Mass = 1f, LimitVelocityMagnitude = 1f, 
                Position = spawnPosition, PreviousPosition = spawnPosition};
        }

        public override void UpdateItem()
        {
            base.UpdateItem();

            AsteroidMovement();

            CheckIfOffscreen();
        }

        private void AsteroidMovement()
        {
            _physicsData.ComputePosition(_direction);
            UnitView.Move(_physicsData.Position);
        }

        private void CheckIfOffscreen()
        {
            var screenPosition = _camera.WorldToScreenPoint(UnitView.transform.position);

            var isOffscreen = screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 ||
                              screenPosition.y > Screen.height;
            if (isOffscreen) RemoveFromGame();
        }

        private void RemoveFromGame()
        {
            //throw new System.NotImplementedException();
        }
        
        public override float ColliderRadius => 1f;
        public override Layer Layer => Layer.Enemy;
    }
}