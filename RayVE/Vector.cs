using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RayVE.Constants;
using static System.Math;

namespace RayVE
{
    public class Vector : IEnumerable<double>, IEquatable<Vector>
    {
        private readonly double[] _values;

        public uint Length
            => Convert.ToUInt32(_values.Length);

        public double this[uint i]
            => _values[i];

        public Vector(params double[] values)
            : this((IEnumerable<double>)values)
        { }

        public Vector(IEnumerable<double> values)
            => _values = values.ToArray();

        public Vector(IEnumerable<int> values)
            : this(values.Select(v => Convert.ToDouble(v)))
        { }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (uint i = 0; i < Length; i++)
                builder.Append(this[i].ToString("F3").PadLeft(8) + Environment.NewLine);

            return builder.ToString();
        }

        public double Magnitude
            => Sqrt(_values.Select(v => Pow(v, 2))
                           .Sum());

        public Vector Normalize()
            => this / Magnitude;

        public Vector Cross(Vector other)
        {
            if (Length != 3 || other.Length != 3)
                throw new DimensionMismatchException();

            return new Vector((this[1] * other[2]) - (this[2] * other[1]),
                              (this[2] * other[0]) - (this[0] * other[2]),
                              (this[0] * other[1]) - (this[1] * other[0]));
        }

        private static Vector CombineElementWise(Vector left, Vector right, Func<double, double, double> combine)
        {
            if (left.Length != right.Length)
                throw new DimensionMismatchException();

            var values = new double[left.Length];

            for (uint i = 0; i < left.Length; i++)
                values[i] = combine(left[i], right[i]);

            return new Vector(values);
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

        public static double operator *(Vector left, Vector right)
            => CombineElementWise(left, right, (l, r) => l * r).Sum();

        public static Vector operator *(Matrix left, Vector right)
        {
            if (left.ColumnCount != right.Length)
                throw new DimensionMismatchException();

            var result = new double[left.RowCount];

            for (uint i = 0; i < left.RowCount; i++)
                result[i] = left.Rows[i] * right;

            return new Vector(result);
        }

        public static Vector operator *(Vector left, Matrix right)
        {
            if (left.Length != right.RowCount)
                throw new DimensionMismatchException();

            var result = new double[right.ColumnCount];

            for (uint i = 0; i < right.ColumnCount; i++)
                result[i] = left * right.Columns[i];

            return new Vector(result);
        }

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
                if (Math.Abs(left[i] - right[i]) > EPSILON)
                    return false;

            return true;
        }

        public static bool operator !=(Vector left, Vector right)
            => !(left == right);
        #endregion

        #region IEnumerable
        public IEnumerator<double> GetEnumerator()
            => ((IEnumerable<double>)_values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable<double>)_values).GetEnumerator();
        #endregion

        #region Equality
        public override bool Equals(object obj)
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
        #endregion

        public static Vector Zero(uint size)
            => new Vector(Enumerable.Repeat(0, Convert.ToInt32(size))
                                    .ToArray());
    }
}
