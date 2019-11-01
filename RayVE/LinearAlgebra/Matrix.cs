using RayVE.Extensions;
using System;
using System.Linq;
using System.Text;
using static RayVE.Constants;
using static System.Math;

namespace RayVE.LinearAlgebra
{
    public sealed class Matrix : IEquatable<Matrix>
    {
        public sealed class RowCollection
        {
            private readonly Matrix _matrix;

            internal RowCollection(Matrix matrix)
                => _matrix = matrix;

            public Vector this[uint i]
                => new Vector(_matrix._values[i]);

            public Vector this[int i]
                => this[Convert.ToUInt32(i)];
        }

        public sealed class ColumnCollection
        {
            private readonly Matrix _matrix;

            internal ColumnCollection(Matrix matrix)
                => _matrix = matrix;

            public Vector this[uint i]
            {
                get
                {
                    var values = new double[_matrix.RowCount];

                    for (uint j = 0; j < _matrix.RowCount; j++)
                        values[j] = _matrix[j, i];

                    return new Vector(values);
                }
            }

            public Vector this[int i]
                => this[Convert.ToUInt32(i)];
        }

        /// <summary>
        /// A ragged array containing the double values of the matrix. Because the assignment
        /// of this array is private, we can safely assume in the use of it that the lengths
        /// of each sub-array are equal.
        /// </summary>
        private readonly double[][] _values;

        public double this[uint i, uint j]
            => _values[i][j];

        public uint RowCount
            => Convert.ToUInt32(_values.Length);

        public uint ColumnCount
            => Convert.ToUInt32(_values[0].Length);

        public bool IsSquare
            => RowCount == ColumnCount;

        public double Determinant
            => RowCount == 2 && ColumnCount == 2
            ? this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0]
            : Rows[0] * GetCofactors(0);

        public bool IsInvertible
            => Determinant != 0;

        public Matrix Cofactors
            => new Matrix(RowCount, ColumnCount, (i, j) => GetCofactor(i, j))
               .Transpose;

        public Matrix Inverse
            => Cofactors / Determinant;

        public Matrix Transpose
        {
            get
            {
                var values = GetRectangularArray(ColumnCount, RowCount);

                for (uint i = 0; i < RowCount; i++)
                    for (uint j = 0; j < ColumnCount; j++)
                        values[j][i] = _values[i][j];

                return new Matrix(values);
            }
        }

        public RowCollection Rows { get; }

        public ColumnCollection Columns { get; }
        
        public Matrix(double[][] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var rows = Convert.ToUInt32(values.Length);
            var columns = Convert.ToUInt32(values[0].Length);

            _values = GetRectangularArray(rows, columns);
            _values = (double[][])values.Clone();

            Rows = new RowCollection(this);
            Columns = new ColumnCollection(this);
        }

        public Matrix(Matrix matrix)
            : this(matrix.RowCount, matrix.ColumnCount, (i, j) => matrix[i, j])
        { }

        internal Matrix(uint rows, uint columns, Func<uint, uint, double> valueSource)
        {
            _values = GetRectangularArray(rows, columns);

            for (uint i = 0; i < rows; i++)
                for (uint j = 0; j < columns; j++)
                    _values[i][j] = valueSource(i, j);

            Rows = new RowCollection(this);
            Columns = new ColumnCollection(this);
        }

        internal Matrix SwapRows(uint rowIndex1, uint rowIndex2)
        {
            if (rowIndex1 > RowCount)
                throw new ArgumentOutOfRangeException("Row index must be less than the maximum row index of the matrix.", nameof(rowIndex1));

            if (rowIndex2 > RowCount)
                throw new ArgumentOutOfRangeException("Row index must be less than the maximum row index of the matrix.", nameof(rowIndex2));

            return Transform((i, j) => i == rowIndex1
                                       ? this[rowIndex2, j]
                                       : i == rowIndex2
                                         ? this[rowIndex1, j]
                                         : this[i, j]);
        }

        internal Matrix ScaleColumn(uint columnIndex, double factor)
        {
            if (columnIndex > ColumnCount)
                throw new ArgumentOutOfRangeException("Column index must be less than the maximum row index of the matrix.", nameof(columnIndex));

            return Transform((i, j) => this[i, j] * (j == columnIndex ? factor : 1.0d));
        }

        internal Matrix Transform(Func<uint, uint, double> transform)
            => Transform(transform, (i, j) => true);

        internal Matrix Transform(Func<uint, uint, double> transform, Func<uint, uint, bool> predicate)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var values = GetRectangularArray(RowCount, ColumnCount);

            for (uint i = 0; i < RowCount; i++)
                for (uint j = 0; j < ColumnCount; j++)
                    values[i][j] = predicate(i, j)
                                 ? transform(i, j)
                                 : this[i, j];

            return new Matrix(values);
        }

        public Matrix GetSubMatrix(uint row, uint column)
            => new Matrix(RowCount - 1, ColumnCount - 1, (i, j) => this[i < row ? i : i + 1, j < column ? j : j + 1]);

        public double GetMinor(uint row, uint column)
            => GetSubMatrix(row, column).Determinant;

        public double GetCofactor(uint row, uint column)
            => GetMinor(row, column) * ((row + column) % 2 == 0 ? 1 : -1);

        public Vector GetCofactors(uint row)
            => new Vector(Enumerable.Range(0, Convert.ToInt32(ColumnCount))
                                    .Select(c => GetCofactor(row, Convert.ToUInt32(c))));
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            for (uint i = 0; i < RowCount; i++)
            {
                for (uint j = 0; j < ColumnCount; j++)
                    builder.Append(this[i, j].ToString("F3").PadLeft(8) + " ");

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        #region Static Factory
        public static Matrix Zero(uint rows, uint columns)
            => new Matrix(rows, columns, (i, j) => 0.0d);

        public static Matrix Identity(uint size)
            => new Matrix(size, size, (i, j) => i == j ? 1.0d : 0.0d);

        public static Matrix Translation(Vector vector)
            => Identity(vector.Length + 1)
               + new Matrix(vector.Length + 1, vector.Length + 1, (i, j) => i != j && j == vector.Length ? vector[i] : 0.0d);

        public static Matrix Scale(Vector scalars)
            => new Matrix(scalars.Length + 1, scalars.Length + 1, (i, j) => i != j ? 0.0d : (i == scalars.Length ? 1.0d : scalars[i]));

        public static Matrix Rotation(Dimension dimension, double angle)
        {
            switch (dimension)
            {
                case Dimension.X:
                    return new Matrix(new[]
                    {
                        new[] { 1.0d, 0.0d,        0.0d,       0.0d },
                        new[] { 0.0d, Cos(angle), -Sin(angle), 0.0d },
                        new[] { 0.0d, Sin(angle),  Cos(angle), 0.0d },
                        new[] { 0.0d, 0.0d,        0.0d,       1.0d }
                    });
                case Dimension.Y:
                    return new Matrix(new[]
                    {
                        new[] { Cos(angle),  0.0d,  Sin(angle), 0.0d },
                        new[] { 0.0d,        1.0d,  0.0d,       0.0d },
                        new[] { -Sin(angle), 0.0d,  Cos(angle), 0.0d },
                        new[] { 0.0d,        0.0d,  0.0d,       1.0d }
                    });
                case Dimension.Z:
                    return new Matrix(new[]
                    {
                        new[] { Cos(angle), -Sin(angle), 0.0d, 0.0d },
                        new[] { Sin(angle),  Cos(angle), 0.0d, 0.0d },
                        new[] { 0.0d,        0.0d,       1.0d, 0.0d },
                        new[] { 0.0d,        0.0d,       0.0d, 1.0d }
                    });
                default:
                    throw new NotImplementedException("Rotation matrices are only implemented for 3D transformations.");
            }
        }

        public static Matrix Shear(Dimension shearDimension, Dimension inProportionTo, double amount)
            => new Matrix(4, 4, (r, c) => (r == (uint)shearDimension) && (c == (uint)inProportionTo)
                                          ? amount
                                          : Identity(4)[r, c]);
        #endregion

        private static double[][] GetRectangularArray(uint rows, uint columns)
        {
            var result = new double[rows][];

            for (uint i = 0; i < rows; i++)
                result[i] = new double[columns];

            return result;
        }

        #region Operators
        public static Matrix operator +(Matrix left, Matrix right)
            => new Matrix(left.RowCount, left.ColumnCount, (i, j) => left[i, j] + right[i, j]);

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            if (right == null)
                throw new ArgumentNullException(nameof(right));

            if (left.ColumnCount != right.RowCount)
                throw new InvalidOperationException("The number of rows for the left matrix must equal the number of columns for the right matrix.");

            var result = GetRectangularArray(left.RowCount, right.ColumnCount);

            for (uint i = 0; i < left.RowCount; i++)
                for (uint j = 0; j < right.ColumnCount; j++)
                    result[i][j] = left.Rows[i] * right.Columns[j];

            return new Matrix(result);
        }

        public static Matrix operator *(double scalar, Matrix matrix)
            => new Matrix(matrix.RowCount, matrix.ColumnCount, (i, j) => scalar * matrix[i, j]);

        public static Matrix operator /(Matrix matrix, double scalar)
            => (1 / scalar) * matrix;

        public static bool operator ==(Matrix left, Matrix right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left.RowCount != right.RowCount || left.ColumnCount != right.ColumnCount)
                return false;

            for (uint i = 0; i < left.RowCount; i++)
                for (uint j = 0; j < left.ColumnCount; j++)
                    if (Abs(left[i, j] - right[i, j]) > EPSILON)
                        return false;

            return true;
        }

        public static bool operator !=(Matrix left, Matrix right)
            => !(left == right);
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj is Matrix matrix)
                return Equals(matrix);

            return false;
        }

        public bool Equals(Matrix other)
            => this == other;

        public override int GetHashCode()
            => _values.SelectMany(r => r.Select(v => v))
                      .Sum()
                      .GetHashCode();
        #endregion
    }
}
