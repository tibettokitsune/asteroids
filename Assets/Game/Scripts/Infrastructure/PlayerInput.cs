using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class PlayerInput : IPlayerInput
    {
        public Vector2 MovementAxis()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public bool IsShoot()
        {
            return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
        }
    }
}