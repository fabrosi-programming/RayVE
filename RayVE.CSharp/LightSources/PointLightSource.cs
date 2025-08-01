using RayVE.LinearAlgebra;
using System;

namespace RayVE.LightSources
{
    public class PointLightSource : ILightSource, IEquatable<PointLightSource>
    {
        public Point3D Position { get; }

        public Color Color { get; }

        public PointLightSource(Point3D position, Color color)
        {
            Position = position;
            Color = color;
        }

        #region Operators
        public static bool operator ==(PointLightSource left, PointLightSource right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Position == right.Position
                && left.Color == right.Color;
        }

        public static bool operator !=(PointLightSource left, PointLightSource right)
            => !(left == right);
        #endregion

        #region Equals
        public override bool Equals(object? obj)
        {
            if (obj is PointLightSource pointLightSource)
                return Equals(pointLightSource);

            return false;
        }

        public override int GetHashCode()
            => (Position, Color).GetHashCode();
        #endregion

        #region IEquatable<PointLightSource>
        public bool Equals(PointLightSource? other)
            => other is not null
            && this == other;
        #endregion
    }
}