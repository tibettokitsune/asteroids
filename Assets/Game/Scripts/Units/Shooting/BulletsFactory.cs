using UnityEngine;

namespace Game.Scripts.Units.Shooting
{
    public interface IBulletsFactory
    {
        BulletPresenter SpawnDefaultBullet(Vector2 position, Vector2 direction);

        BulletPresenter SpawnLaserBullet(Vector2 position, Vector2 direction);
    }
    public class BulletsFactory : IBulletsFactory
    {
        private ViewsConfiguration _configuration;
        public BulletsFactory()
        {
            _configuration = Resources.Load<ViewsConfiguration>("UnitsViewAsset");
        }
        public BulletPresenter SpawnDefaultBullet(Vector2 position, Vector2 direction)
        {
            var bullet = new BulletPresenter(null, position, direction, _configuration.defaultProjectileConfiguration);
            return bullet;
        }

        public BulletPresenter SpawnLaserBullet(Vector2 position, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}