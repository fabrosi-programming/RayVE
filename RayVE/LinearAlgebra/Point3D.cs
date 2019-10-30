namespace RayVE.LinearAlgebra
{
    public sealed class Point3D : Vector
    {
        public Point3D(double x, double y, double z)
            : base(x, y, z, 1.0d)
        { }

        public override double Magnitude => new Vector(this[0], this[1], this[2]).Magnitude;
    }
}
