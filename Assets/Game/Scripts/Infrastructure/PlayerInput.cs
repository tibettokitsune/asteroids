using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Infrastructure
{
    public class PlayerInput : IPlayerInput
    {
        public bool IsSimpleShoot() 
            =>  Keyboard.current.pKey.isPressed;

        public bool IsLaserShoot()
            =>  Keyboard.current.lKey.wasPressedThisFrame;

        private float VerticalAxis()
        {
            if (Keyboard.current.wKey.isPressed) return 1f;
            if (Keyboard.current.sKey.isPressed) return -1f;
            return 0f;
        }

        private float HorizontalAxis()
        {
            if (Keyboard.current.dKey.isPressed) return 1f;
            if (Keyboard.current.aKey.isPressed) return -1f;
            return 0f;
        }
        
        public Vector2 MovementAxis() => new(HorizontalAxis(), VerticalAxis());

    }
}