using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public interface IPlayerInput
    {
        Vector2 MovementAxis();

        bool IsShoot();
    }
}