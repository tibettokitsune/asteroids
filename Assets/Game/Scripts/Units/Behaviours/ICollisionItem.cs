using System;
using UnityEngine;

namespace Game.Scripts.Units.Behaviours
{
    public interface ICollisionItem
    {
        public Action OnCollide { get; } 
        public float ColliderRadius { get; }
        public Layer Layer { get; }
        public Vector2 Position { get; }
    }
}