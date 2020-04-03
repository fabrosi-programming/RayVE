using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public sealed class Vector3D : Vector
    {
        //private Vector _vector;

        public double X
            => this[0];

        public double Y
            => this[1];

        public double Z
            => this[2];

        public Vector3D(double x, double y, double z, bool normalize = false)
            : this(new Vector(x, y, z, 0.0d), normalize)
        { }

        public Vector3D(Vector vector, bool normalize)
            : this(normalize ? new Vector(vector.Take(3)).Normalize() : vector)
        { }

        public Vector3D(Vector vector)
            : base(vector.Take(3).Append(0.0d))
        {
            //_vector = new Vector(vector.Take(3).Append(0.0d));
        }

        public Vector3D Cross(Vector3D other)
            => new Vector3D((this[1] * other[2]) - (this[2] * other[1]),
                            (this[2] * other[0]) - (this[0] * other[2]),
                            (this[0] * other[1]) - (this[1] * other[0]));

        public override Vector AsVector()
            => new Vector(Take(3));

        public static Vector3D Zero
            => new Vector3D(0, 0, 0);

        #region Operators

        public static Vector3D operator *(double scalar, Vector3D vector)
            => new Vector3D(scalar * (Vector)vector);

        public static Vector3D operator -(Vector3D vector)
            => new Vector3D(-vector.AsVector());

        #endregion Operators
    }
}