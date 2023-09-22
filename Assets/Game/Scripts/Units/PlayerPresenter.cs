using Game.Scripts.Infrastructure;
using UnityEngine;

namespace Game.Scripts.Units
{
    public class PlayerPresenter : UnitPresenter
    {
        private IPlayerInput _playerInput;
        private Vector2 _currentPosition;
        private float _currentAngle;
        private const float RotationSpeed = 80f;
        public PlayerPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, IPlayerInput playerInput) : base(unitConfiguration, spawnPosition)
        {
            _playerInput = playerInput;
        }

        public override void UpdateItem()
        {
            base.UpdateItem();

            _currentAngle += RotationSpeed * Time.deltaTime * -_playerInput.MovementAxis().x;
            UnitView.Rotate(_currentAngle);
            
            
        }
        
    }
}