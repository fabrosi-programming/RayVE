using RayVE.LinearAlgebra;
using System;
using static System.Math;

namespace RayVE.Materials
{
    public class RingPattern : PatternBase
    {
        private readonly Color _color1;
        private readonly Color _color2;

        public RingPattern(Color color1, Color color2)
            : this(color1, color2, Matrix.Identity(4))
        { }

        public RingPattern(Color color1, Color color2, Matrix transformation)
            : base(transformation)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color ColorAt(Point3D localPoint)
            => Floor(Sqrt(Pow(localPoint.X, 2) + Pow(localPoint.Y, 2))) % 2 == 0
            ? _color1
            : _color2;
    }
}
