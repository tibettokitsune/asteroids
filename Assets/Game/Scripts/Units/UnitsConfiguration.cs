using UnityEngine;

namespace Game.Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitsConfiguration", menuName = "Configs/UnitsAsset")]
    public class UnitsConfiguration : ScriptableObject
    {
        public UnitConfiguration playerConfiguration;
        public UnitConfiguration asteroidConfiguration;
        public UnitConfiguration alienConfiguration;
    }
}