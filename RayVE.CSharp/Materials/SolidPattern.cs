using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Materials
{
    public class SolidPattern : PatternBase
    {
        private readonly Color _color;

        public SolidPattern(Color color)
            : base(Matrix.Identity(4))
            => _color = color;

        protected override Color ColorAt(Point3D localPoint) => _color;

        #region Operators

        public static bool operator ==(SolidPattern left, SolidPattern right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left._color != right._color)
                return false;

            return true;
        }

        public static bool operator !=(SolidPattern left, SolidPattern right)
            => !(left == right);

        #endregion

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is SolidPattern pattern)
                return Equals(pattern);

            return false;
        }

        public bool Equals(SolidPattern? other)
            => other is not null
            && this == other;

        public override int GetHashCode()
            => _color.GetHashCode();

        #endregion
    }
}
