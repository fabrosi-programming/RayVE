using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public sealed class Vector3D
    {
        private readonly Vector _vector;

        public double X
            => _vector[0];

        public double Y
            => _vector[1];

        public double Z
            => _vector[2];

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
            => new((_vector[1] * other._vector[2]) - (_vector[2] * other._vector[1]),
                   (_vector[2] * other._vector[0]) - (_vector[0] * other._vector[2]),
                   (_vector[0] * other._vector[1]) - (_vector[1] * other._vector[0]));

        public Vector AsVector()
            => new(_vector.Take(3));

        public Vector3D Normalize()
            => new(_vector.Normalize());

        public Vector3D Reflect(Vector3D normal)
            => new(AsVector().Reflect(normal.AsVector()));

        public Vector3D Scale(Vector3D scalars)
            => new(_vector.Scale(scalars.AsVector()));

        public static Vector3D Zero
            => new(0, 0, 0);

        #region Operators

        public static Vector3D Multiply(double scalar, Vector3D vector)
            => new(scalar * vector._vector);

        public static Vector3D operator *(double scalar, Vector3D vector)
            => Multiply(scalar, vector);    

        public static Vector3D Multiply(Vector3D vector, double scalar)
            => Multiply(scalar, vector);

        public static Vector3D operator *(Vector3D vector, double scalar)
            => Multiply(scalar, vector);

        public static double Multiply(Vector3D left, Vector3D right)
            => left.AsVector() * right.AsVector();

        public static double operator *(Vector3D left, Vector3D right)
            => Multiply(left, right);

        public static Vector3D Multiply(Matrix matrix, Vector3D vector)
            => new(matrix * vector._vector);

        public static Vector3D operator *(Matrix matrix, Vector3D vector)
            => Multiply(matrix, vector);

        public static Vector3D Multiply(Vector3D vector, Matrix matrix)
            => new(vector._vector * matrix);

        public static Vector3D operator *(Vector3D left, Matrix right)
            => Multiply(left, right);

        public static Vector3D Negate(Vector3D vector)
            => new(-vector.AsVector());

        public static Vector3D operator -(Vector3D vector)
            => Negate(vector);    

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
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