using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Units.Shooting
{
    public class BulletPresenter : ICollisionItem, IUpdateItem, IKillable
    {
        public Action OnCollide { get; }
        
        protected BulletView View;
        public float ColliderRadius => 1f;
        public Layer Layer => Layer.Player;
        public Vector2 Position => View.transform.position;
        private Vector2 _direction;
        private BulletConfiguration _configuration;
        
        public BulletPresenter(Action onCollide, Vector2 position, Vector2 direction, BulletConfiguration bulletConfiguration)
        {
            OnCollide = onCollide;
            _direction = direction;
            _configuration = bulletConfiguration;
            View = Object.Instantiate(bulletConfiguration.view, position, Quaternion.identity);
        }

        public void UpdateItem()
        {
            View.transform.Translate(_direction * Time.deltaTime * _configuration.movementSpeed);
        }

        public void GetDamage()
        {
            OnCollide?.Invoke();
            Object.Destroy(View.gameObject);
        }
    }

    public interface IKillable
    {
        void GetDamage();
    }
}