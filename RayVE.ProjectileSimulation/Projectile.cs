using RayVE.LinearAlgebra;
using System.Diagnostics;

namespace RayVE.ProjectileSimulation
{
    [DebuggerDisplay("({Position[0]},{Position[1]},{Position[2]}) x ({Velocity[0]},{Velocity[1]},{Velocity[2]})")]
    public class Projectile
    {
        public readonly Vector2D Position;
        public readonly Vector2D Velocity;
        public readonly Color Color;

        public Projectile(Vector2D position, Vector2D velocity, Color color)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
        }
    }
}