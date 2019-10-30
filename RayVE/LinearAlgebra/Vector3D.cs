using System.Linq;

namespace RayVE.LinearAlgebra
{
    public sealed class Vector3D : Vector
    {
        public Vector3D(double x, double y, double z)
            : base(x, y, z, 0.0d)
        { }

        public Vector3D(Vector vector)
            : base(vector.Take(3).Append(0.0d))
        { }
    }
}
