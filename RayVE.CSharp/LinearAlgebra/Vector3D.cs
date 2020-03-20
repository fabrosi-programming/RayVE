using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]}, {this[2]})")]
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

        public Vector3D Cross(Vector3D other)
            => new Vector3D((this[1] * other[2]) - (this[2] * other[1]),
                            (this[2] * other[0]) - (this[0] * other[2]),
                            (this[0] * other[1]) - (this[1] * other[0]));

        #region Operators

        public static Vector3D operator *(double scalar, Vector3D vector)
            => new Vector3D(scalar * (Vector)vector);

        public static Vector3D operator -(Vector3D vector)
            => new Vector3D(-(Vector)vector);

        #endregion Operators
    }
}