using System;
using Game.Scripts.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Units
{
    public class UnitPresenter : IUpdateItem, ICollisionItem
    {
        protected UnitView UnitView;

        private Vector2 _position;
        private float _angle;

        protected UnitConfiguration Configuration;
        public UnitPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, Action onCollide)
        {
            UnitView = Object.Instantiate(unitConfiguration.view, spawnPosition, Quaternion.identity);
            Configuration = unitConfiguration;
            OnCollide = onCollide;
        }

        public virtual void UpdateItem(){}
        public Action OnCollide { get; }
        public virtual float ColliderRadius => 1f;
        public virtual Layer Layer { get; }
        public Vector2 Position => UnitView.transform.position;
    }

    public interface ICollisionItem
    {
        public Action OnCollide { get; } 
        public float ColliderRadius { get; }
        public Layer Layer { get; }
        public Vector2 Position { get; }
    }

    public enum Layer
    {
        Player, Enemy
    }
}