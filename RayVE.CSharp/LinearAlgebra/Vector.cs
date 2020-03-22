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

        public uint Length
            => Convert.ToUInt32(_values.Length);

        public double this[uint i]
            => _values[i];

        public Vector(params double[] values)
            : this((IEnumerable<double>)values)
        { }

        public Vector(IEnumerable<double> values)
            => _values = values.ToArray();

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

            return Enumerable.Range(0, (int)left.Length)
                             .Select(i => Convert.ToUInt32(i))
                             .Select(i => combine(left[i], right[i]))
                             .ToVector();
        }

        #region Operators

        public static Vector operator +(Vector left, Vector right)
            => CombineElementWise(left, right, (l, r) => l + r);

        public static Vector operator -(Vector left, Vector right)
            => left + (-right);

        public static Vector operator -(Vector vector)
            => new Vector(vector._values.Select(v => -v)
                                        .ToArray());

        public static Vector operator *(double scalar, Vector vector)
            => new Vector(vector._values.Select(v => v * scalar)
                                        .ToArray());

        public static Vector operator *(Vector vector, double scalar)
            => scalar * vector;

        public static double operator *(Vector left, Vector right)
            => CombineElementWise(left, right, (l, r) => l * r).Sum();

        public static Vector operator *(Matrix left, Vector right)
        {
            if (left.ColumnCount != right.Length)
                throw new DimensionMismatchException("Left matrix column count does not match right vector length.");

            return Enumerable.Range(0, (int)left.RowCount)
                             .Select(i => Convert.ToUInt32(i))
                             .Select(i => left.Rows[i] * right)
                             .ToVector();
        }

        public static Vector operator *(Vector left, Matrix right)
        {
            if (left.Length != right.RowCount)
                throw new DimensionMismatchException("Left vector length does not match right matrix row count.");

            return Enumerable.Range(0, (int)right.ColumnCount)
                             .Select(i => Convert.ToUInt32(i))
                             .Select(i => left * right.Columns[i])
                             .ToVector();
        }

        [SuppressMessage("Style", "IDE0047:Remove unnecessary parentheses", Justification = "Parentheses aid clarity of intent.")]
        public static Vector operator /(Vector vector, double scalar)
            => (1 / scalar) * vector;

        public static bool operator ==(Vector left, Vector right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left.Length != right.Length)
                return false;

            for (uint i = 0; i < left.Length; i++)
                if (Abs(left[i] - right[i]) > EPSILON)
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

        public bool Equals(Vector other)
            => this == other;

        public override int GetHashCode()
            => _values.Sum()
                      .GetHashCode();

        #endregion Equality

        public static Vector Zero(uint size)
            => new Vector(Enumerable.Repeat(0, Convert.ToInt32(size))
                                    .ToArray());
    }
}