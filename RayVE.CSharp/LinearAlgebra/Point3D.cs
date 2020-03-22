using System.Diagnostics;
using System.Linq;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public sealed class Point3D : Vector
    {
        public double X
            => this[0];

        public double Y
            => this[1];

        public double Z
            => this[2];

        public Point3D(double x, double y, double z)
            : base(x, y, z, 1.0d)
        { }

        public Point3D(Vector vector)
            : base(vector.Take(3).Append(1.0d))
        { }

        public override double Magnitude
            => new Vector(this[0], this[1], this[2]).Magnitude;

        public override Vector AsVector()
            => new Vector(Take(3));

        #region Operators

        public static Point3D operator -(Point3D point)
            => new Point3D(-point.AsVector());

        public static Vector3D operator -(Point3D left, Point3D right)
            => new Vector3D((Vector)left - right);

        public static Point3D operator +(Point3D point, Vector3D vector)
            => new Point3D((Vector)point + vector);

        #endregion Operators
    }
}