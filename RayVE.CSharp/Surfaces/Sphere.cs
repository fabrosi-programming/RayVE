using RayVE.LinearAlgebra;
using RayVE.Materials;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static System.Math;

namespace RayVE.Surfaces
{
    public class Sphere : ISurface, IEquatable<Sphere>
    {
        private readonly Point3D _center;
        private readonly Matrix _transformation;
        private readonly Matrix _inverseTransformation;
        private readonly Matrix _transposeInverseTransformation;

        public Sphere()
            : this(new Point3D(0, 0, 0))
        { }

        public Sphere(Point3D center)
            : this(center, Matrix.Identity(4), new PhongMaterial())
        { }

        public Sphere(Matrix transformation)
            : this(new Point3D(0, 0, 0), transformation, new PhongMaterial())
        { }

        public Sphere(IMaterial material)
            : this(new Point3D(0, 0, 0), material)
        { }

        public Sphere(Point3D center, IMaterial material)
            : this(center, Matrix.Identity(4), material)
        { }

        public Sphere(Matrix transformation, IMaterial material)
            : this(new Point3D(0, 0, 0), transformation, material)
        { }

        public Sphere(Point3D center, Matrix transformation, IMaterial material)
        {
            _center = center;
            _transformation = transformation;
            _inverseTransformation = _transformation.Inverse;
            _transposeInverseTransformation = _transformation.Inverse.Transpose;
            Material = material;
        }

        private IEnumerable<double> GetIntersections(Ray ray)
        {
            var transformedRay = _inverseTransformation * ray;
            var connectOrigins = transformedRay.Origin - _center;
            var a = transformedRay.Direction * transformedRay.Direction; // more precise than Pow(ray.Direction.Magnitude, 2)
            var b = 2 * transformedRay.Direction * connectOrigins;
            var c = (connectOrigins * connectOrigins) - 1; // more precise than Pow(connectOrigins.Direction.Magnitude, 2)
            var discriminant = Algebra.Discriminant(a, b, c);

            if (discriminant < 0)
                return new List<double>().ToImmutableList();

            return new[]
            {
                (-b - Sqrt(discriminant)) / (2 * a),
                (-b + Sqrt(discriminant)) / (2 * a)
            }.ToImmutableList();
        }

        #region Operators
        public static bool operator ==(Sphere left, Sphere right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left._center == right._center
                && left._inverseTransformation == right._inverseTransformation
                && left.Material.Equals(right.Material); // no support for custom == operator on interfaces
        }

        public static bool operator !=(Sphere left, Sphere right)
            => !(left == right);
        #endregion

        #region ISurface
        public IMaterial Material { get; }

        public Intersections Intersect(Ray ray)
            => new Intersections(
                GetIntersections(ray).Select(i => new Intersection(i, this, ray)));

        public Vector3D GetNormal(Point3D point)
        {
            var objectPoint = _inverseTransformation * point;
            var objectNormal = objectPoint - _center;
            return (_transposeInverseTransformation * objectNormal).Normalize();
        }

        public ISurface WithMaterial(IMaterial material)
            => new Sphere(_center, _transformation, material);
        #endregion ISurface

        #region Equals
        public override bool Equals(object? obj)
        {
            if (obj is Sphere sphere)
                return Equals(sphere);

            return false;
        }
        #endregion

        #region IEquatable<Sphere>
        public bool Equals(Sphere other)
            => this == other;
        #endregion
    }
}