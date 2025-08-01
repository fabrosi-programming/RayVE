using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.Materials
{
    public class GradientPattern : PatternBase
    {
        private readonly Color _color1;
        private readonly Color _color2;

        public GradientPattern(Color color1, Color color2)
            : this(color1, color2, Matrix.Identity(4))
        { }

        public GradientPattern(Color color1, Color color2, Matrix transformation)
            : base(transformation)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color ColorAt(Point3D localPoint)
        {
            var colorDistance = _color2 - _color1;
            var fraction = localPoint.X - Math.Floor(localPoint.X);
            return _color1 + (colorDistance * fraction);
        }
    }
}
