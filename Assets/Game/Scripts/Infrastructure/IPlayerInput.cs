using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public interface IPlayerInput
    {
        Vector2 MovementAxis();

        bool IsSimpleShoot();
        bool IsLaserShoot();
    }
}