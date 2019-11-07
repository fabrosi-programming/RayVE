using System.Linq;

namespace RayVE.LinearAlgebra
{
    public sealed class Point3D : Vector
    {
        public Point3D(double x, double y, double z)
            : base(x, y, z, 1.0d)
        { }

        public Point3D(Vector vector)
            : base(vector.Take(3).Append(1.0d))
        { }

        public override double Magnitude
            => new Vector(this[0], this[1], this[2]).Magnitude;

        #region Operators

        public static Vector3D operator -(Point3D left, Point3D right)
            => new Vector3D((Vector)left - (Vector)right);

        public static Point3D operator +(Point3D point, Vector3D vector)
            => new Point3D((Vector)point + (Vector)vector);

        #endregion Operators
    }
}