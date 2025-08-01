using RayVE.LinearAlgebra;
using RayVE.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RayVE.ProjectileSimulation
{
    internal class Program
    {
        private static void Main()
        {
            var gravity = new Vector2D(0.0d, -0.1d);
            var wind = new Vector2D(-0.05d, 0.0d);
            var environment = new Environment(gravity, wind);
            var simulation = new Simulation(environment);
            var width = 1920;
            var height = 1080;
            var canvas = new Canvas(width, height);

            simulation.Projectiles.AddRange(new List<Projectile>()
            {
                new Projectile(new Vector2D(0.01d, 0.01d), 10 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D(0.01d, 0.01d), 11 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D(0.01d, 0.01d), 12 * new Vector2D(1.0d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D(0.01d, 0.01d), 13 * new Vector2D(1.0d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D(0.01d, 0.01d), 14 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D(0.01d, 0.01d), 15 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D(0.01d, 0.01d), 10.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D(0.01d, 0.01d), 11.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D(0.01d, 0.01d), 12.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D(0.01d, 0.01d), 13.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D(0.01d, 0.01d), 14.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D(0.01d, 0.01d), 15.5 * new Vector2D(1.0d, 1.0d).Normalize(), Color.Blue),

                new Projectile(new Vector2D( width - 0.01d, 0.01d), 10 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 11 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 12 * new Vector2D(0.5d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 13 * new Vector2D(0.5d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 14 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 15 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 10.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 11.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Red),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 12.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 13.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.White),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 14.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Blue),
                new Projectile(new Vector2D( width - 0.01d, 0.01d), 15.5 * new Vector2D(0.5d, 1.0d).Normalize(), Color.Blue)
            });

            Debug.WriteLine("Simulating projectile paths.");

            while (simulation.Projectiles.Any(p => p.Position.Y > 0.0d))
            {
                foreach (var projectile in simulation.Projectiles)
                {
                    var x = (uint)projectile.Position.X - 1;
                    var y = canvas.Height - (uint)projectile.Position.Y - 1;
                    canvas[x, y] += projectile.Color;
                }

                simulation.Tick(0.0625);
            }

            Debug.WriteLine("Writing to file.");

            var document = new PPMDocument(canvas, 255);
            var filePath = $"C:\\temp\\RayVE\\ProjectileSimulation\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, document.ToString());

            Debug.WriteLine("Done.");
        }
    }
}