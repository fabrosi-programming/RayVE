using RayVE.Extensions;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.SphereDrawing
{
    class Program
    {
        static void Main(string[] args)
        {
            var color = Color.Red;
            var origin = new Point3D(0, 0, -5);
            var canvasSize = 1000;
            var canvasDepth = 10;
            var scale = 7.0d / (double)canvasSize;

            var rays = from x in Enumerable.Range(-canvasSize / 2, canvasSize)
                       from y in Enumerable.Range(-canvasSize / 2, canvasSize)
                       select (Index: (x, y), Ray: new Ray(origin, new Vector3D((double)x * scale, (double)y * scale, canvasDepth)));

            var sphere = new Sphere(Matrix.Shear(Dimension.Y, Dimension.X, 2.0));
            
            var hits = rays.AsParallel().Select(r => (r.Index, Hit: sphere.Intersect(r.Ray).GetHit()));
            var canvas = new Canvas(canvasSize, canvasSize);

            foreach (var ray in hits)
                canvas[ray.Index.x + canvasSize / 2, ray.Index.y + canvasSize / 2] = ray.Hit.HasValue ? Color.Red : Color.Black;

            var filePath = $"C:\\temp\\RayVE\\SphereDrawing\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, canvas.ToPPM(255));
        }
    }
}
