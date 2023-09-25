using System.Collections.Generic;
using Game.Scripts.Infrastructure;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Physics;

namespace Game.Scripts.Units.Shooting.ShootingSystems
{
    public class ShootingSystem : IUpdateItem
    {
        protected readonly IPlayerInput PlayerInput;
        protected readonly IBulletsFactory BulletsFactory;
        protected readonly UnitPresenter SourceUnit;
        protected readonly List<BulletPresenter> Bullets = new List<BulletPresenter>();

        public ShootingSystem(IPlayerInput input, IBulletsFactory bulletsFactory, UnitPresenter source)
        {
            PlayerInput = input;
            BulletsFactory = bulletsFactory;
            SourceUnit = source;
        }
        public virtual void UpdateItem()
        {
            
        }

        public virtual List<UnitPresenter> ComputeCollision(List<UnitPresenter> units)
        {
            return null;
        }
        
        protected virtual void Shoot(){}
    }
}