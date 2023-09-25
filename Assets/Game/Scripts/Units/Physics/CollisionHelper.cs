using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Units.Behaviours;
using Game.Scripts.Units.Shooting;
using UnityEngine;

namespace Game.Scripts.Units.Physics
{
    public static class CollisionHelper
    {
        public static bool CalculateCollisionForTarget(UnitPresenter targetItem, List<UnitPresenter> allItems)
        {
            return allItems.Where(x =>
                    Vector2.Distance(targetItem.Position, x.Position) <= targetItem.ColliderRadius)
                .Any(x => x.Layer != targetItem.Layer);
        }
        
        public static bool CalculateCollisionForTarget(BulletPresenter targetItem, List<UnitPresenter> allItems)
        {
            return allItems.Where(x =>
                    Vector2.Distance(targetItem.Position, x.Position) <= targetItem.ColliderRadius)
                .Any(x => x.Layer != targetItem.Layer);
        }
        
        public static List<UnitPresenter> CollidedUnits(BulletPresenter targetItem, List<UnitPresenter> allItems)
        {
            return allItems.Where(x =>
                    Vector2.Distance(targetItem.Position, x.Position) <= targetItem.ColliderRadius)
                .Where(x => x.Layer != targetItem.Layer).ToList();
        }
    }
}