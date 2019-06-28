using System;
using System.Diagnostics;
using static System.Math;
using static RayVE.Constants;

namespace RayVE
{
    [DebuggerDisplay("({X},{Y},{Z})")]
    public struct Vector3D : IEquatable<Vector3D>
    {
        public Point3D PointFromOrigin { get; }

        public double X => PointFromOrigin.X;
        public double Y => PointFromOrigin.Y;
        public double Z => PointFromOrigin.Z;

        public Vector3D(double x, double y, double z)
            => PointFromOrigin = new Point3D(x, y, z);

        public Vector3D(Point3D point)
            : this(point.X, point.Y, point.Z)
        { }

        public Vector3D(Vector3D vector)
            : this(vector.PointFromOrigin)
        { }

        public double Magnitude
            => Sqrt(this * this);

        public Vector3D Normalize()
            => this / Magnitude;

        public Vector3D Cross(Vector3D other)
            => new Vector3D((Y * other.Z) - (Z * other.Y),
                            (Z * other.X) - (X * other.Z),
                            (X * other.Y) - (Y * other.X));

        #region Operators
        public static Point3D operator +(Point3D point, Vector3D vector)
            => new Point3D(point.X + vector.X,
                           point.Y + vector.Y,
                           point.Z + vector.Z);

        public static Vector3D operator +(Vector3D left, Vector3D right)
            => new Vector3D(left.X + right.X,
                            left.Y + right.Y,
                            left.Z + right.Z);

        public static Point3D operator -(Point3D point, Vector3D vector)
            => point + (-vector);

        public static Vector3D operator -(Vector3D left, Vector3D right)
            => left + (-right);

        public static double operator *(Vector3D left, Vector3D right)
            => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);

        public static Vector3D operator *(double scalar, Vector3D vector)
            => new Vector3D(scalar * vector.PointFromOrigin);

        public static Vector3D operator /(Vector3D vector, double scalar)
            => (1 / scalar) * vector;

        public static Vector3D operator -(Vector3D vector)
            => new Vector3D(-vector.PointFromOrigin);

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(left, null))
                return false;

            if (ReferenceEquals(right, null))
                return false;

            return left.PointFromOrigin == right.PointFromOrigin;
        }

        public static bool operator !=(Vector3D left, Vector3D right)
            => !(left == right);
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj is Vector3D vector)
                return Equals(vector);

            return false;
        }

        public bool Equals(Vector3D other)
        {
            if (Abs(X - other.X) > EPSILON
                || Abs(Y - other.Y) > EPSILON
                || Abs(Z - other.Z) > EPSILON)
                return false;

            return true;
        }

        public override int GetHashCode()
            => X.GetHashCode()
             + Y.GetHashCode()
             + Z.GetHashCode();
        #endregion

        public static Vector3D Zero
            => new Vector3D(Point3D.Origin);
    }
}
