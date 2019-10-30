using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace RayVE.Surfaces
{
    public class Sphere : ISurface
    {
        private readonly Vector _origin;

        public Sphere()
            : this(new Vector(0, 0, 0))
        { }
        public Sphere(Vector origin)
        {
            _origin = origin;
        }

        private Vector ConnectOrigins(Ray ray)
            => ray.Origin - _origin;

        private double a(Ray ray)
            => ray.Direction * ray.Direction;

        private double b(Ray ray)
            => 2 * ray.Direction * ConnectOrigins(ray);

        private double c(Ray ray)
            => (ConnectOrigins(ray) * ConnectOrigins(ray)) - 1;

        private double Discriminant(Ray ray)
            => Algebra.Discriminant(a(ray), b(ray), c(ray));

        private bool Intersects(Ray ray)
            => Discriminant(ray) < 0
               ? false
               : true;

        private IEnumerable<double> GetIntersections(Ray ray)
            => Intersects(ray)
               ? new[]
                 {
                     (-b(ray) - Sqrt(Discriminant(ray))) / 2 * a(ray),
                     (-b(ray) + Sqrt(Discriminant(ray))) / 2 * a(ray)
                 }.ToImmutableList()
               : new List<double>().ToImmutableList();

        #region ISurface
        public IEnumerable<Intersection> Intersect(Ray ray)
            => GetIntersections(ray).Select(i => new Intersection(i, this));
        #endregion
    }
}
