using RayVE;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.ProjectileSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var gravity = new Vector3D(0.0d, -0.1d, 0.0d);
            var wind = new Vector3D(-0.01d, 0.0d, 0.0d);
            var environment = new Environment(gravity, wind);
            var simulation = new Simulation(environment);
            var canvas = new Canvas(1920, 1080);

            var projectil = new Projectile(Point3D.Origin, new Vector3D(1.0d, 1.0d, 0.0d).Normalize());
            simulation.Projectiles.Add(projectil);

            while (simulation.Projectiles.Any(p => p.Position.Y > 0.0d))
            {
                foreach (var projectile in simulation.Projectiles)
                {
                    var x = Convert.ToInt32(projectile.Position.X);
                    var y = canvas.Height - Convert.ToInt32(projectile.Position.Y);
                    canvas[x, y] = Color.White;
                }

                simulation.Tick();
            }

            var filePath = $"C:\\temp\\RayVE\\ProjectileSimulation\\{DateTime.Now:yyyyMMdd_hhmmss}.ppm";
            File.WriteAllText(filePath, canvas.ToPPM(255));
        }
    }
}