using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Units.Shooting
{
    public class DefaultBullet : BulletPresenter
    {
        public DefaultBullet(Action onCollide, Vector2 position, Vector2 direction, BulletConfiguration bulletConfiguration) : base(onCollide, position, direction, bulletConfiguration)
        {
        }

        public override void GetDamage()
        {
            base.GetDamage();
            OnCollide?.Invoke();
            Object.Destroy(View.gameObject);
        }
    }
}