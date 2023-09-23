using Game.Scripts.Infrastructure;
using UnityEngine;

namespace Game.Scripts.Units
{
    public class PlayerPresenter : UnitPresenter
    {
        private readonly IPlayerInput _playerInput;
        private readonly Camera _camera;
        
        private float _currentAngle;
        private float _forwardImpulse;
        
        private const float RotationSpeed = 80f;
        private const float Braking = 0.02f;
        private const float MovementTreshhold = 0.01f;
        public PlayerPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, IPlayerInput playerInput) : base(unitConfiguration, spawnPosition)
        {
            _playerInput = playerInput;
            _camera = Camera.main;
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
            if (Mathf.Abs( _playerInput.MovementAxis().y) > MovementTreshhold)
            {
                _forwardImpulse += Time.deltaTime * Configuration.movementSpeed * _playerInput.MovementAxis().y;
            }
            else
            {
                if (Mathf.Abs(_forwardImpulse) > MovementTreshhold)
                {
                    if (_forwardImpulse > 0)
                    {
                        _forwardImpulse -= Time.deltaTime * Braking;
                    }

                    if (_forwardImpulse < 0)
                    {
                        _forwardImpulse -= Time.deltaTime * Braking;
                    }
                }
                else
                {
                    _forwardImpulse = 0f;
                }
            }

            UnitView.ForwardMove(_forwardImpulse);
        }

        private void Rotation()
        {
            _currentAngle += RotationSpeed * Time.deltaTime * -_playerInput.MovementAxis().x;
            Debug.Log(_currentAngle);
            UnitView.Rotate(_currentAngle);
        }
    }
}