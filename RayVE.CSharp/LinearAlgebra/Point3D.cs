using RayVE.Extensions;
using System;
using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public sealed class Point3D : IEquatable<Point3D>
    {
        private Vector _vector;

        public double X
            => _vector[0];

        public double Y
            => _vector[1];

        public double Z
            => _vector[2];

        public Point3D(double x, double y, double z)
            : this(new Vector(x, y, z))
        { }

        public Point3D(Vector vector)
        {
            _vector = new Vector(vector.Take(3).Append(1.0d));
        }

        public double Magnitude
            => AsVector().Magnitude;

        public Vector AsVector()
            => new(_vector.Take(3));

        public Point3D Translate(Vector translation)
            => new(_vector.Translate(translation));
        
        public Point3D Rotate(Dimension dimension, double angle)
            => new(_vector.Rotate(dimension, angle));

        public Point3D Scale(Vector3D scalars)
            => new(_vector.Scale(scalars.AsVector()));

        public Point3D Translate(Vector3D translation)
            => new(_vector.Translate(translation.AsVector()));

        public static Point3D Zero
            => new(0, 0, 0);

        #region Operators

        public static Point3D Negate(Point3D point)
            => new(-point.AsVector());

        public static Point3D operator -(Point3D point)
            => Negate(point);

        public static Vector3D Subtract(Point3D left, Point3D right)
            => new(left._vector - right._vector);

        public static Vector3D operator -(Point3D left, Point3D right)
            => Subtract(left, right);

        public static Point3D Add(Point3D point, Vector3D vector)
            => new(point.AsVector() + vector.AsVector());

        public static Point3D operator +(Point3D point, Vector3D vector)
            => Add(point, vector);

        public static Point3D Multiply(Matrix left, Point3D right)
        {
            if (left.ColumnCount != 4)
                throw new DimensionMismatchException("Left matrix column count does not match right vector length.");

            var vector = Enumerable.Range(0, (int)left.RowCount)
                                   .Select(i => Convert.ToUInt32(i))
                                   .Select(i => left.Rows[i] * right._vector)
                                   .ToVector();

            return new Point3D(vector);
        }

        public static Point3D operator *(Matrix left, Point3D right)
            => Multiply(left, right);

        public static Point3D Multiply(Point3D left, Matrix right)
        {
            if (right.RowCount != 4)
                throw new DimensionMismatchException("Left vector length does not match right matrix row count.");

            var vector = Enumerable.Range(0, (int)right.ColumnCount)
                                   .Select(i => Convert.ToUInt32(i))
                                   .Select(i => left._vector * right.Columns[i])
                                   .ToVector();

            return new Point3D(vector);
        }

        public static Point3D operator *(Point3D left, Matrix right)
            => Multiply(left, right);

        public static bool operator ==(Point3D left, Point3D right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left._vector != right._vector)
                return false;

            return true;
        }

        public static bool operator !=(Point3D left, Point3D right)
            => !(left == right);

        #endregion Operators

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is Point3D vector)
                return Equals(vector);

            return false;
        }

        public override int GetHashCode()
            => _vector.GetHashCode();

        #endregion

        #region IEquatable<Point3D>
        public bool Equals(Point3D? other)
            => other is not null
            && this == other;
        #endregion
    }
}