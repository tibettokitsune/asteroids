using Game.Scripts.Infrastructure;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Units
{
    public class PlayerPresenter : UnitPresenter
    {
        private GameplayHUDPanel _hud;
        private readonly IPlayerInput _playerInput;
        private readonly Camera _camera;
        
        private float _currentAngle;
        private float _forwardImpulse;
        private Vector2 _cashPosition;
        private SimplePhysicsData _physicsData;
        private const float RotationSpeed = 100f;
        private const float Braking = 0.1f;
        private const float MovementTreshhold = 0.1f;
        
        public PlayerPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, 
            IPlayerInput playerInput, GameplayHUDPanel hud) : base(unitConfiguration, spawnPosition)
        {
            _playerInput = playerInput;
            _camera = Camera.main;
            _hud = hud;
            _physicsData = new SimplePhysicsData() {Mass = 1f};
        }

        public override void UpdateItem()
        {
            base.UpdateItem();
            
            Rotation();
            ForwardMovement();
            OffscreenTeleportation();
        }

        private void OffscreenTeleportation()
        {
            var screenPosition = _camera.WorldToScreenPoint(UnitView.transform.position);

            if (screenPosition.x < 0) screenPosition.x += Screen.width;
            if (screenPosition.x > Screen.width) screenPosition.x -= Screen.width;
            if (screenPosition.y < 0) screenPosition.y += Screen.height;
            if (screenPosition.y > Screen.height) screenPosition.y -= Screen.height;
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            UnitView.Move(worldPosition);
            _physicsData.Position = worldPosition;
            _physicsData.PreviousPosition = worldPosition;
        }

        private void ForwardMovement()
        {
            _physicsData.ComputePosition(
                UnitView.ViewForward() * _playerInput.MovementAxis().y);
            UnitView.Move(_physicsData.Position);
            _hud.UpdateVelocity(_physicsData.Velocity.magnitude);
            _hud.UpdateCoordinates(_physicsData.Position);
        }

        private void Rotation()
        {
            _currentAngle += RotationSpeed * Time.deltaTime * -_playerInput.MovementAxis().x;
            UnitView.Rotate(_currentAngle);
            _hud.UpdateRotationAngle(_currentAngle);
        }
    }
}