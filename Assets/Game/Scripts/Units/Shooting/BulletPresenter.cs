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
        public Action<Vector2> OnCollide { get; }
        
        protected BulletView View;
        public float ColliderRadius => 1f;
        public Layer Layer => Layer.Player;
        public Vector2 Position => View.transform.position;
        private Vector2 _direction;
        private BulletConfiguration _configuration;
        
        public BulletPresenter(Action<Vector2> onCollide, Vector2 position, Vector2 direction, BulletConfiguration bulletConfiguration)
        {
            OnCollide = onCollide;
            _direction = direction;
            _configuration = bulletConfiguration;
            var angle = Vector2.SignedAngle(Vector2.up, direction);
            View = Object.Instantiate(bulletConfiguration.view, position, Quaternion.LookRotation(direction, Vector3.forward));
        }

        public void UpdateItem()
        {
            View.transform.position += (Vector3)(_direction * _configuration.movementSpeed * Time.deltaTime);
        }

        public virtual void GetDamage()
        {
            
        }
    }

    public interface IKillable
    {
        void GetDamage();
    }
}