using RayVE.LinearAlgebra;
using RayVE.Materials;
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
        private readonly Point3D _center;

        private readonly Matrix _inverseTransformation;

        private readonly Matrix _transposeInverseTransformation;

        public Sphere()
            : this(new Point3D(0, 0, 0))
        { }

        public Sphere(Point3D center)
            : this(center, Matrix.Identity(center.Length), new NoMaterial())
        { }

        public Sphere(Matrix transformation)
            : this(new Point3D(0, 0, 0), transformation, new NoMaterial())
        { }

        public Sphere(IMaterial material)
            : this(new Point3D(0, 0, 0), material)
        { }

        public Sphere(Point3D center, IMaterial material)
            : this(center, Matrix.Identity(center.Length), material)
        { }

        public Sphere(Matrix transformation, IMaterial material)
            : this(new Point3D(0, 0, 0), transformation, material)
        { }

        public Sphere(Point3D center, Matrix transformation, IMaterial material)
        {
            _center = center;
            _inverseTransformation = transformation.Inverse;
            _transposeInverseTransformation = transformation.Inverse.Transpose;
            Material = material;
        }

        private IEnumerable<double> GetIntersections(Ray ray)
        {
            var transformedRay = _inverseTransformation * ray;
            var connectOrigins = transformedRay.Origin - _center;
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
        public IMaterial Material { get; }

        public IEnumerable<Intersection> Intersect(Ray ray)
            => GetIntersections(ray).Select(i => new Intersection(i, this));

        public Vector3D GetNormal(Point3D point)
        {
            var objectPoint = _inverseTransformation * point;
            var objectNormal = objectPoint - _center;
            return new Vector3D(_transposeInverseTransformation * objectNormal, true);
        }
        #endregion
    }
}
