using Game.Scripts.Units.Shooting;
using UnityEngine;

namespace Game.Scripts.Units
{
    [CreateAssetMenu(fileName = "UnitsConfiguration", menuName = "Configs/UnitsAsset")]
    public class ViewsConfiguration : ScriptableObject
    {
        public UnitConfiguration playerConfiguration;
        public UnitConfiguration asteroidConfiguration;
        public UnitConfiguration alienConfiguration;

        [Space(30)] 
        public BulletConfiguration defaultProjectileConfiguration;
        public BulletConfiguration laserConfiguration;

    }
}