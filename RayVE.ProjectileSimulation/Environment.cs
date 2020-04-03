using RayVE.LinearAlgebra;

namespace RayVE.ProjectileSimulation
{
    public class Environment
    {
        public readonly Vector2D Gravity;
        public readonly Vector2D Wind;

        public Vector2D TotalEffect
            => Gravity + Wind;

        public Environment(Vector2D gravity, Vector2D wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
    }
}