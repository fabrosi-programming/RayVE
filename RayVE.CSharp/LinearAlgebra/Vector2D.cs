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
            => new Vector(_vector.Take(2));

        public Vector2D Normalize()
            => new Vector2D(_vector.Normalize());

        public Vector2D Scale(Vector2D scalars)
            => new Vector2D(_vector.Scale(scalars.AsVector()));

        #region Operators

        public static Vector2D operator *(double scalar, Vector2D vector)
            => new Vector2D(scalar * vector._vector);

        public static Vector2D operator +(Vector2D left, Vector2D right)
            => new Vector2D(left._vector + right._vector);

        #endregion

        public static Vector2D Zero
            => new Vector2D(0, 0);
    }
}