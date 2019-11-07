using RayVE.Surfaces;
using System;

namespace RayVE
{
    public class Intersection : IEquatable<Intersection>
    {
        public double Distance { get; }

        public ISurface Surface { get; }

        public Intersection(double distance, ISurface surface)
        {
            Distance = distance;
            Surface = surface;
        }

        #region Operators

        public static bool operator ==(Intersection left, Intersection right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            if (left.Distance != right.Distance)
                return false;

            if (left.Surface != right.Surface)
                return false;

            return true;
        }

        public static bool operator !=(Intersection left, Intersection right)
            => !(left == right);

        #endregion Operators

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is Intersection intersection)
                return Equals(intersection);

            return false;
        }

        public bool Equals(Intersection other)
            => this == other;

        public override int GetHashCode()
            => Distance.GetHashCode() + Surface.GetHashCode();

        #endregion Equality
    }
}