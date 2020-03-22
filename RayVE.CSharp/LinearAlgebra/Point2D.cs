using System.Diagnostics;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]})")]
    public sealed class Point2D : Vector
    {
        public Point2D(double x, double y)
            : base(x, y, 1.0d)
        { }

        public override Vector AsVector()
            => new Vector(Take(2));
    }
}