using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Physics;
using Game.Scripts.Units.Shooting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Units.Behaviours
{
    public class UnitPresenter : IUpdateItem, ICollisionItem, IKillable
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

        public Vector2 Direction => UnitView.ViewForward();
        public void GetDamage()
        {
            OnCollide.Invoke();
            Object.Destroy(UnitView.gameObject);
        }
    }
}