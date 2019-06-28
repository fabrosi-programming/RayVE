namespace RayVE.ProjectileSimulation
{
    public class Environment
    {
        public readonly Vector Gravity;
        public readonly Vector Wind;

        public Vector TotalEffect
            => Gravity + Wind;

        public Environment(Vector gravity, Vector wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
    }
}