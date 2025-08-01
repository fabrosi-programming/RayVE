using RayVE.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static RayVE.Constants;
using static System.Math;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({System.String.Join(\", \", _values),nq})")]
    public class Vector : IEquatable<Vector>
    {
        private readonly double[] _values;

        public virtual Vector AsVector()
            => this;

        public uint Length { get; }

        public double this[uint i]
            => _values[i];

        public Vector(params double[] values)
            : this((IEnumerable<double>)values)
        { }

        public Vector(IEnumerable<double> values)
        {
            _values = values.ToArray();
            Length = Convert.ToUInt32(_values.Length); // pre-calculate since this is definitely going to be called
        }

        public Vector(Vector vector)
            => _values = vector._values.ToArray();

        public Vector(IEnumerable<int> values)
            : this(values.Select(v => Convert.ToDouble(v)))
        { }

        public override string ToString()
            => String.Join(Environment.NewLine, Enumerable.Range(0, (int)Length)
                                                          .Select(i => Convert.ToUInt32(i))
                                                          .Select(i => this[i].ToString("F3").PadLeft(8)));

        public virtual double Magnitude
            => Sqrt(_values.Select(v => Pow(v, 2))
                           .Sum());

        public Vector Normalize()
            => this / Magnitude;

        public Vector Translate(Vector translation)
            => Matrix.Translation(translation) * this;

        public Vector Scale(Vector scalars)
            => Matrix.Scale(scalars) * this;

        public Vector Rotate(Dimension dimension, double angle)
            => Matrix.Rotation(dimension, angle) * this;

        public Vector Reflect(Vector normal)
            => this - (normal * (2 * (this * normal)));

        public double Sum()
            => _values.Sum();

        public IEnumerable<double> Take(int count)
            => _values.Take(count);

        private static Vector CombineElementWise(Vector left, Vector right, Func<double, double, double> combine)
        {
            if (left.Length != right.Length)
                throw new DimensionMismatchException();

            // Method 1 appears to perform marginally better than Method 2
            return CombineElementWise_Method1(left, right, combine);
        }

        private static Vector CombineElementWise_Method1(Vector left, Vector right, Func<double, double, double> combine)
        {
            var values = new double[left.Length];

            for (uint i = 0; i < left.Length; i++)
                values[i] = combine(left[i], right[i]);

            return new Vector(values);
        }

        private static Vector CombineElementWise_Method2(Vector left, Vector right, Func<double, double, double> combine)
            => MoreEnumerable
                .UIntRange(0, left.Length)
                .Select(i => combine(left[i], right[i]))
                .ToVector();

        #region Operators
        public static Vector Add(Vector left, Vector right)
            => CombineElementWise(left, right, (l, r) => l + r);

        public static Vector operator +(Vector left, Vector right)
            => Add(left, right);

        public static Vector Subtract(Vector left, Vector right)
            => left + (-right);

        public static Vector operator -(Vector left, Vector right)
            => Subtract(left, right);

        public static Vector Negate(Vector vector)
            => new(
                vector
                ._values
                .Select(v => -v)
                .ToArray());

        public static Vector operator -(Vector vector)
            => Negate(vector);

        public static Vector Multiply(double scalar, Vector vector)
            => new(
                vector
                ._values
                .Select(v => v * scalar)
                .ToArray());

        public static Vector operator *(double scalar, Vector vector)
            => Multiply(scalar, vector);

        public static Vector operator *(Vector vector, double scalar)
            => Multiply(scalar, vector);

        public static double Multiply(Vector left, Vector right)
            => CombineElementWise(left, right, (l, r) => l * r).Sum();

        public static double operator *(Vector left, Vector right)
            => Multiply(left, right);

        public static Vector Multiply(Matrix left, Vector right)
        {
            if (left.ColumnCount != right.Length)
                throw new DimensionMismatchException("Left matrix column count does not match right vector length.");

            return Enumerable.Range(0, (int)left.RowCount)
                             .Select(i => Convert.ToUInt32(i))
                             .Select(i => left.Rows[i] * right)
                             .ToVector();
        }

        public static Vector operator *(Matrix left, Vector right)
            => Multiply(left, right);

        public static Vector Multiply(Vector left, Matrix right)
        {
            if (left.Length != right.RowCount)
                throw new DimensionMismatchException("Left vector length does not match right matrix row count.");

            return Enumerable.Range(0, (int)right.ColumnCount)
                             .Select(i => Convert.ToUInt32(i))
                             .Select(i => left * right.Columns[i])
                             .ToVector();
        }

        public static Vector operator *(Vector left, Matrix right)
            => Multiply(left, right);

        [SuppressMessage("Style", "IDE0047:Remove unnecessary parentheses", Justification = "Parentheses aid clarity of intent.")]
        public static Vector Divide(Vector vector, double scalar)
            => (1 / scalar) * vector;

        public static Vector operator /(Vector vector, double scalar)
            => Divide(vector, scalar);

        public static bool operator ==(Vector left, Vector right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left.Length != right.Length)
                return false;

            for (uint i = 0; i < left.Length; i++)
                if (Abs(left[i] - right[i]) > Epsilon)
                    return false;

            return true;
        }

        public static bool operator !=(Vector left, Vector right)
            => !(left == right);

        #endregion Operators

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is Vector vector)
                return Equals(vector);

            return false;
        }

        public override int GetHashCode()
            => _values.Sum()
                      .GetHashCode();

        #endregion Equality

        #region IEquatable<Vector>
        public bool Equals(Vector? other)
            => other is not null
            && this == other;
        #endregion

        public static Vector Zeros(uint size)
            => new(Enumerable.Repeat(0, Convert.ToInt32(size)).ToArray());
    }
}