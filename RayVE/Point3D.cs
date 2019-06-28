using RayVE.Extensions;
using System;
using System.Diagnostics;
using static System.Math;
using static RayVE.Constants;

namespace RayVE
{
    [DebuggerDisplay("({X},{Y},{Z})")]
    public struct Point3D : IEquatable<Point3D>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(Point3D point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public Point3D Clamp(double Xmin = double.NegativeInfinity, double Xmax = double.PositiveInfinity
                            ,double Ymin = double.NegativeInfinity, double Ymax = double.PositiveInfinity
                            ,double Zmin = double.NegativeInfinity, double Zmax = double.PositiveInfinity)
            => new Point3D(X.Clamp(Xmin, Xmax), Y.Clamp(Ymin, Ymax), Z.Clamp(Zmin, Zmax));

        #region Operators
        public static Vector3D operator -(Point3D left, Point3D right)
            => new Vector3D(left.X - right.X,
                            left.Y - right.Y,
                            left.Z - right.Z);

        public static Point3D operator -(Point3D point)
            => new Point3D(-1 * point);

        public static Point3D operator *(double scalar, Point3D point)
            => new Point3D(scalar * point.X, scalar * point.Y, scalar * point.Z);

        public static Point3D operator /(Point3D point, double scalar)
            => (1 / scalar) * point;

        public static bool operator ==(Point3D left, Point3D right)
        {
            if (ReferenceEquals(left, right))
                return true;

            // no null check because Point3D is a struct

            if (left.X == right.X
                && left.Y == right.Y
                && left.Z == right.Z)
                return true;

            if (Abs(left.X - right.X) < EPSILON
                && Abs(left.Y - right.Y) < EPSILON
                && Abs(left.Z - right.Z) < EPSILON)
                return true;

            return false;
        }

        public static bool operator !=(Point3D left, Point3D right)
            => !(left == right);
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj is Point3D point)
                return Equals(point);

            return false;
        }

        public bool Equals(Point3D other)
            => this == other;

        public override int GetHashCode()
            => X.GetHashCode()
             + Y.GetHashCode()
             + Z.GetHashCode();
        #endregion

        public static Point3D Origin
            => new Point3D(0.0d, 0.0d, 0.0d);
    }
}
