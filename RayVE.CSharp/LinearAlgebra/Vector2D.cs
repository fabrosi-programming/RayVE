using System;
using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]})")]
    public sealed class Vector2D
    {
        private Vector _vector;

        public double X
            => _vector[0];

        public double Y
            => _vector[1];

        public Vector2D(double x, double y)
            : this(new Vector(x, y, 0.0))
        { }

        public Vector2D(Vector vector)
            => _vector = new Vector(vector.Take(3).Append(0.0));

        public Vector AsVector()
            => new(_vector.Take(2));

        public Vector2D Normalize()
            => new(_vector.Normalize());

        public Vector2D Scale(Vector2D scalars)
            => new(_vector.Scale(scalars.AsVector()));

        #region Operators

        public static Vector2D Multiply(double scalar, Vector2D vector)
            => new(scalar * vector._vector);

        public static Vector2D operator *(double scalar, Vector2D vector)
            => Multiply(scalar, vector);

        public static Vector2D Add(Vector2D left, Vector2D right)
            => new(left._vector + right._vector);

        public static Vector2D operator +(Vector2D left, Vector2D right)
            => Add(left, right);

        #endregion

        public static Vector2D Zero
            => new(0, 0);
    }
}