using RayVE.Surfaces;
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
    public class Sphere : SurfaceBase, IEquatable<Sphere>
    {
        private readonly Point3D _center;

        public Sphere()
            : this(new Point3D(0, 0, 0))
        { }

        public Sphere(Point3D center)
            : this(center, Matrix.Identity(4), PhongMaterial.Default)
        { }

        public Sphere(Matrix transformation)
            : this(new Point3D(0, 0, 0), transformation, PhongMaterial.Default)
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
            : base(transformation, material)
        {
            _center = center;
        }

        #region Operators
        public static bool operator ==(Sphere left, Sphere right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left._center == right._center
                && left.InverseTransformation == right.InverseTransformation
                && left.Material.Equals(right.Material); // no support for custom == operator on interfaces
        }

        public static bool operator !=(Sphere left, Sphere right)
            => !(left == right);
        #endregion

        #region SurfaceBase

        internal override IEnumerable<double> GetIntersectionsLocal(Ray localizedRay)
        {
            var connectOrigins = localizedRay.Origin - _center;
            var a = localizedRay.Direction * localizedRay.Direction; // more precise than Pow(ray.Direction.Magnitude, 2)
            var b = 2 * localizedRay.Direction * connectOrigins;
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

        internal override Vector3D GetNormalLocal(Point3D localizedPoint)
            => localizedPoint - _center;

        public override ISurface WithMaterial(IMaterial material)
            => new Sphere(_center, Transformation, material);
        #endregion

        #region Equals
        public override bool Equals(object? obj)
        {
            if (obj is Sphere sphere)
                return Equals(sphere);

            return false;
        }

        public override int GetHashCode() => (_center, Transformation, Material).GetHashCode();
        #endregion

        #region IEquatable<Sphere>
        public bool Equals(Sphere? other)
            => other is not null
            && this == other;
        #endregion
    }
}