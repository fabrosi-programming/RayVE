using System.Diagnostics;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]})")]
    public sealed class Vector2D : Vector
    {
        public Vector2D(double x, double y)
            : base(x, y, 0.0d)
        { }
    }
}