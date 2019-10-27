using RayVE.LinearAlgebra;
using System.Collections.Generic;

namespace RayVE.ProjectileSimulation
{
    public class Simulation
    {
        public readonly Environment Environment;
        public List<Projectile> Projectiles = new List<Projectile>(); // TODO: consider keeping a history of projectile positions

        public Simulation(Environment environment)
            => Environment = environment;

        public void Tick(double granularity = 1.0d)
        {
            var updatedProjectiles = new List<Projectile>();

            foreach (var projectile in Projectiles)
            {
                if (projectile.Velocity == Vector.Zero(2))
                {
                    updatedProjectiles.Add(projectile);
                    break;
                }

                var position = (projectile.Position + granularity * projectile.Velocity);

                Vector velocity;
                if (projectile.Position[1] == 0)
                    velocity = projectile.Velocity + granularity * Environment.Wind;
                else
                    velocity = projectile.Velocity + granularity * Environment.TotalEffect;

                updatedProjectiles.Add(new Projectile(position, velocity, projectile.Color));
            }

            Projectiles = updatedProjectiles;
        }
    }
}