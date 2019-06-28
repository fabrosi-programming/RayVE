using RayVE;
using System.Diagnostics;

namespace RayVE.ProjectileSimulation
{
    [DebuggerDisplay("({Position.X},{Position.Y},{Position.Z}) x ({Velocity.X},{Velocity.Y},{Velocity.Z})")]
    public class Projectile
    {
        public readonly Point3D Position;
        public readonly Vector3D Velocity;

        public Projectile(Point3D position, Vector3D velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }
}