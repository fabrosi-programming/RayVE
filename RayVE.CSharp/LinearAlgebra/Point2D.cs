using System.Diagnostics;

namespace RayVE.LinearAlgebra
{
    [DebuggerDisplay("({this[0]}, {this[1]})")]
    public sealed class Point2D
    {
        private Vector _vector;

        public Point2D(double x, double y)
        {
            _vector = new Vector(x, y, 1.0d);
        }

        public Vector AsVector()
            => new(_vector.Take(2));

        public static Point2D Zero
            => new(0, 0);
    }
}