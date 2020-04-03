using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public sealed class Vector3D
    {
        private Vector _vector;

        public double X
            => _vector[0];

        public double Y
            => _vector[1];

        public double Z
            => _vector[2];

        public uint Length
            => _vector.Length;

        public double Magnitude
            => _vector.Magnitude;

        public Vector3D(double x, double y, double z, bool normalize = false)
            : this(new Vector(x, y, z, 0.0d), normalize)
        { }

        public Vector3D(Vector vector, bool normalize)
            : this(normalize ? new Vector(vector.Take(3)).Normalize() : vector)
        { }

        public Vector3D(Vector vector)
            => _vector = new Vector(vector.Take(3).Append(0.0d));

        public Vector3D Cross(Vector3D other)
            => new Vector3D((_vector[1] * other._vector[2]) - (_vector[2] * other._vector[1]),
                            (_vector[2] * other._vector[0]) - (_vector[0] * other._vector[2]),
                            (_vector[0] * other._vector[1]) - (_vector[1] * other._vector[0]));

        public Vector AsVector()
            => new Vector(_vector.Take(3));

        public Vector3D Normalize()
            => new Vector3D(_vector.Normalize());

        public Vector3D Reflect(Vector3D normal)
            => new Vector3D(AsVector().Reflect(normal.AsVector()));

        public Vector3D Scale(Vector3D scalars)
            => new Vector3D(_vector.Scale(scalars.AsVector()));

        public static Vector3D Zero
            => new Vector3D(0, 0, 0);

        #region Operators

        public static Vector3D operator *(double scalar, Vector3D vector)
            => new Vector3D(scalar * vector._vector);

        public static double operator *(Vector3D left, Vector3D right)
            => left.AsVector() * right.AsVector();

        public static Vector3D operator *(Matrix left, Vector3D right)
            => new Vector3D(left * right._vector);

        public static Vector3D operator *(Vector3D left, Matrix right)
            => new Vector3D(left._vector * right);

        public static Vector3D operator -(Vector3D vector)
            => new Vector3D(-vector.AsVector());

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left.Length != right.Length)
                return false;

            if (left._vector != right._vector)
                return false;

            return true;
        }

        public static bool operator !=(Vector3D left, Vector3D right)
            => !(left == right);

        #endregion Operators

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is Vector3D vector)
                return Equals(vector);

            return false;
        }

        public bool Equals(Vector3D other)
            => this == other;

        public override int GetHashCode()
            => _vector.GetHashCode();

        #endregion
    }
}