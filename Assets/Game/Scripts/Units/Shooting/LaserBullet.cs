using System;
using UnityEngine;

namespace Game.Scripts.Units.Shooting
{
    public class LaserBullet : BulletPresenter
    {
        public LaserBullet(Action<Vector2> onCollide, Vector2 position, Vector2 direction, 
            BulletConfiguration bulletConfiguration) : base(onCollide, position, direction, bulletConfiguration)
        {
        }

        public override void GetDamage()
        {
            base.GetDamage();
        }
    }
}