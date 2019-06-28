namespace RayVE.ProjectileSimulation
{
    public class Environment
    {
        public readonly Vector3D Gravity;
        public readonly Vector3D Wind;

        public Vector3D TotalEffect
            => Gravity + Wind;

        public Environment(Vector3D gravity, Vector3D wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
    }
}