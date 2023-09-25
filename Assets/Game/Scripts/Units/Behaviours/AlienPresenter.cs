using System;
using Game.Scripts.Units.Physics;
using UnityEngine;

namespace Game.Scripts.Units.Behaviours
{
    public class AlienPresenter : UnitPresenter, ICollisionItem
    {
        private readonly UnitPresenter _persecutionTarget;
        private readonly SimplePhysicsData _physicsData;
        public AlienPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition,Action onCollide,
            UnitPresenter persecutionTarget) : base(unitConfiguration, spawnPosition, onCollide)
        {
            _persecutionTarget = persecutionTarget;
            _physicsData = new SimplePhysicsData()
            {
                Mass = 1f, 
                Position = spawnPosition, 
                PreviousPosition = spawnPosition, 
                LimitVelocityMagnitude = 1f
            };

        }

        public override void UpdateItem()
        {
            base.UpdateItem();

            FollowTarget();
        }

        private void FollowTarget()
        {
            _physicsData.ComputePosition((_persecutionTarget.Position - Position).normalized);
            UnitView.Move(_physicsData.Position);
        }

        
        public override float ColliderRadius => 1f;
        public override Layer Layer => Layer.Enemy;
    }
}