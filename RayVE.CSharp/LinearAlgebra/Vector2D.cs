using System.Diagnostics;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]})")]
    public sealed class Vector2D : Vector
    {
        public Vector2D(double x, double y)
            : base(x, y, 0.0d)
        { }

        public override Vector AsVector()
            => new Vector(Take(2));

        public static Vector2D Zero
            => new Vector2D(0, 0);
    }
}