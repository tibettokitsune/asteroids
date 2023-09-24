using UnityEngine;

namespace Game.Scripts.Units
{
    public class AlienPresenter : UnitPresenter
    {
        private IMovingObject _persecutionTarget;
        private SimplePhysicsData _physicsData;
        public AlienPresenter(UnitConfiguration unitConfiguration, Vector2 spawnPosition, 
            IMovingObject persecutionTarget) : base(unitConfiguration, spawnPosition)
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
    }
}