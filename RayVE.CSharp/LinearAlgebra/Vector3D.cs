using System.Linq;

namespace RayVE.LinearAlgebra
{
    public sealed class Vector3D : Vector
    {
        public Vector3D(double x, double y, double z, bool normalize = false)
            : this(new Vector(x, y, z, 0.0d), normalize)
        { }

        public Vector3D(Vector vector, bool normalize)
            : this(normalize ? new Vector(vector.Take(3)).Normalize() : vector)
        { }

        public Vector3D(Vector vector)
            : base(vector.Take(3).Append(0.0d))
        { }

        #region Operators

        public static Vector3D operator *(double scalar, Vector3D vector)
            => new Vector3D(scalar * (Vector)vector);

        public static Vector3D operator -(Vector3D vector)
            => new Vector3D(-(Vector)vector);

        #endregion Operators
    }
}