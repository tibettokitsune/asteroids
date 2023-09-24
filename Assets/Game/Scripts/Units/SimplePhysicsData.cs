using UnityEngine;

namespace Game.Scripts.Units
{
    public class SimplePhysicsData
    {
        public Vector2 Velocity { get; private set; }
    
        public Vector2 Position { get; set; }
        public float Mass;
        public float  LimitVelocityMagnitude;
        public Vector2 PreviousPosition{ get; set; }
        private Vector2 PreviousVelocity { get; set; }


        public void ComputePosition(Vector2 force)
        {
            //f = m * a
            var acceleration = force / Mass;
            //v = v0 + a * dt
            Velocity = PreviousVelocity + acceleration * Time.deltaTime;
            Velocity = Vector3.ClampMagnitude(Velocity, LimitVelocityMagnitude);
            //s = s0 + v0 * dt + a * dt * dt / 2 = s0 + (v0 + v) * dt
            Position = PreviousPosition + (PreviousVelocity + Velocity) * Time.deltaTime;
            PreviousPosition = Position;
            PreviousVelocity = Velocity;
        }
    }
}