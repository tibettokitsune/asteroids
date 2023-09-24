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

            UnitView.Move(_camera.ScreenToWorldPoint(screenPosition));
        }

        private void ForwardMovement()
        {
            _physicsData.ComputePosition(
                UnitView.ViewForward() * _playerInput.MovementAxis().y);
            UnitView.Move(_physicsData.Position);
            _hud.UpdateVelocity(_physicsData.Velocity.magnitude);
        }

        private void Rotation()
        {
            _currentAngle += RotationSpeed * Time.deltaTime * -_playerInput.MovementAxis().x;
            UnitView.Rotate(_currentAngle);
            _hud.UpdateRotationAngle(_currentAngle);
        }
    }
}

public class SimplePhysicsData
{
    public float Mass;
    public Vector2 PreviousPosition;
    public Vector2 Position;
    public Vector2 PreviousVelocity { get; private set; }
    public Vector2 Velocity { get; private set; }

    private const float LimitVelocityMagnitude = 10f;

    public void ComputePosition(Vector2 force)
    {
        var acceleration = force / Mass;
        Velocity = PreviousVelocity + acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, LimitVelocityMagnitude);
        Position = PreviousPosition + (PreviousVelocity + Velocity) * Time.deltaTime;
        PreviousPosition = Position;
        PreviousVelocity = Velocity;
    }
}