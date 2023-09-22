using UnityEngine;

namespace Game.Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitConfiguration", menuName = "Configs/UnitConfiguration")]
    public class UnitConfiguration : ScriptableObject
    {
        public UnitView view;
        public float movementSpeed;
    }
}