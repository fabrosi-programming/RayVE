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
        private readonly Point3D _origin;

        private readonly Matrix _transformation;
        public Sphere()
            : this(new Point3D(0, 0, 0))
        { }
        public Sphere(Point3D origin)
            : this(origin, Matrix.Identity(origin.Length))
        { }

        public Sphere(Matrix transformation)
            : this(new Point3D(0, 0, 0), transformation)
        { }

        public Sphere(Point3D origin, Matrix transformation)
        {
            _origin = origin;
            _transformation = transformation;
        }

        private Vector3D ConnectOrigins(Ray ray)
            => ray.Origin - _origin;

        private IEnumerable<double> GetIntersections(Ray ray)
        {
            var transformedRay = _transformation.Inverse * ray;
            var connectOrigins = transformedRay.Origin - _origin;
            var a = transformedRay.Direction * transformedRay.Direction; // more precise than Pow(ray.Direction.Magnitude, 2)
            var b = 2 * transformedRay.Direction * connectOrigins;
            var c = connectOrigins * connectOrigins - 1; // more precise than Pow(connectOrigins.Direction.Magnitude, 2)
            var discriminant = Algebra.Discriminant(a, b, c);

            if (discriminant < 0)
                return new List<double>().ToImmutableList();

            return new[]
            {
                (-b - Sqrt(discriminant)) / (2 * a),
                (-b + Sqrt(discriminant)) / (2 * a)
            }.ToImmutableList();
        }
        #region ISurface
        public IEnumerable<Intersection> Intersect(Ray ray)
            => GetIntersections(ray).Select(i => new Intersection(i, this));
        #endregion
    }
}
