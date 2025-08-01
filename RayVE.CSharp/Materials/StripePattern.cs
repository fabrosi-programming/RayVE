using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.Materials
{
    public class StripePattern : PatternBase
    {
        private readonly Color _color1;
        private readonly Color _color2;

        public StripePattern(Color color1, Color color2)
            : this(color1, color2, Matrix.Identity(4))
        { }

        public StripePattern(Color color1, Color color2, Matrix transformation)
            : base(transformation)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color ColorAt(Point3D localPoint)
            => (Math.Floor(localPoint.X) % 2) switch
            {
                0.0 => _color1,
                _ => _color2
            };

        #region Operators

        public static bool operator ==(StripePattern left, StripePattern right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left._color1 != right._color1)
                return false;

            if (left._color2 != right._color2)
                return false;

            return true;
        }

        public static bool operator !=(StripePattern left, StripePattern right)
            => !(left == right);

        #endregion

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is StripePattern pattern)
                return Equals(pattern);

            return false;
        }

        public bool Equals(StripePattern? other)
            => other is not null
            && this == other;

        public override int GetHashCode()
            => (_color1, _color2).GetHashCode();

        #endregion
    }
}
