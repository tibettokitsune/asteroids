using UnityEngine;

namespace Game.Scripts.Units.Shooting
{
    
    [CreateAssetMenu(fileName = "BulletConfiguration", menuName = "Configs/BulletConfiguration")]
    public class BulletConfiguration : ScriptableObject
    {
        public BulletView view;
        public float movementSpeed;
    }
}