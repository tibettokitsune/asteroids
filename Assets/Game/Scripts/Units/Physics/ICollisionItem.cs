using System;
using UnityEngine;

namespace Game.Scripts.Units.Physics
{
    
    public enum Layer
    {
        Player, Enemy
    }
    public interface ICollisionItem
    {
        public Action OnCollide { get; } 
        public float ColliderRadius { get; }
        public Layer Layer { get; }
        public Vector2 Position { get; }
    }
}