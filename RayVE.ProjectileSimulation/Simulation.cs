using System.Collections.Generic;

namespace RayVE.ProjectileSimulation
{
    public class Simulation
    {
        public readonly Environment Environment;
        public List<Projectile> Projectiles = new List<Projectile>(); // TODO: consider keeping a history of projectile positions

        public Simulation(Environment environment)
        {
            Environment = environment;
        }

        public void Tick()
        {
            var updatedProjectiles = new List<Projectile>();

            foreach (var projectile in Projectiles)
            {
                if (projectile.Velocity == Vector3D.Zero)
                {
                    updatedProjectiles.Add(projectile);
                    break;
                }

                var position = (projectile.Position + projectile.Velocity).Clamp(Ymin: 0.0d);

                Vector3D velocity;
                if (projectile.Position.Y == 0)
                    velocity = projectile.Velocity + Environment.Wind;
                else
                    velocity = projectile.Velocity + Environment.TotalEffect;

                updatedProjectiles.Add(new Projectile(position, velocity));
            }

            Projectiles = updatedProjectiles;
        }
    }
}