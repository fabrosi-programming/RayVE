using System;
using System.IO;
using System.Linq;
using RayVE;

namespace RayVE.SphereDrawing
{
    internal class Program
    {
        private static void Main()
        {
            var origin = new Point3D(0, 0, -5);
            var canvasSize = 1000;
            var canvasDepth = 10;
            var scale = 7.0d / (double)canvasSize;
            var ambientColor = new Color(0.1, 0.1, 0.1);

            var rays = from x in Enumerable.Range(-canvasSize / 2, canvasSize)
                       from y in Enumerable.Range(-canvasSize / 2, canvasSize)
                       select (Index: (x, y), Ray: new Ray(origin, new Vector3D(x * scale, y * scale, canvasDepth)));

            var sphereMaterial = Material.NewPhong(
                //new StripePattern(ColorModule.Predefined.Red, ColorModule.Predefined.Green),
                0.1,
                0.9,
                0.9,
                200.0);
            var sphere = Surface.NewSphere(sphereMaterial);

            var lightPosition = new Point3D(-10, 10, -10);
            var lightColor = ColorModule.Predefined.White;
            var lightSource = LightSource.NewPointLightSource(lightPosition, lightColor);

            var hits = rays.AsParallel()
                           .Select(r => (r.Index, r.Ray, Hit: SurfaceModule.intersect(sphere, r.Ray).GetNearestHit()));

            var canvas = new Canvas(canvasSize, canvasSize, 255);

            foreach (var hit in hits)
            {
                var xPixel = hit.Index.x + (canvasSize / 2);
                var yPixel = -hit.Index.y + (canvasSize / 2);

                if (!hit.Hit.HasValue)
                    canvas[xPixel, yPixel] = ambientColor;
                else
                {
                    var surface = hit
                        .Hit
                        .Value
                        .Surface;
                    var point = hit
                        .Ray
                        .GetPosition(hit
                            .Hit
                            .Value
                            .Distance);
                    var normalVector = surface.GetNormal(point);
                    var eyeVector = -hit
                        .Ray
                        .Direction;

                    canvas[xPixel, yPixel] = surface
                        .Material
                        .Illuminate(
                            point,
                            surface,
                            eyeVector,
                            normalVector,
                            lightSource);
                }
            }

            var document = new PPMDocument(canvas, 255);
            var filePath = $"C:\\temp\\RayVE\\SphereDrawing\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, document.ToString());
        }
    }
}