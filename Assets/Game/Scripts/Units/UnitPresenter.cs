using Game.Scripts.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Units
{
    public class UnitPresenter : IUpdateItem, IMovingObject
    {
        protected UnitView UnitView;

        private Vector2 _position;
        private float _angle;

        protected UnitConfiguration Configuration;
        public UnitPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition)
        {
            UnitView = Object.Instantiate(unitConfiguration.view, spawnPosition, Quaternion.identity);
            Configuration = unitConfiguration;
        }

        public virtual void UpdateItem()
        {
        }

        public Vector2 Position => UnitView.transform.position;
    }
}