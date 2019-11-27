using System;

namespace RayVE
{
    public struct UDouble : IEquatable<UDouble>
    {
        private readonly double _value;

        public UDouble(double value)
            => _value = value < 0 ? 0 : value;

        public static implicit operator double(UDouble u)
            => u._value;

        public static implicit operator UDouble(double d)
            => new UDouble(d);

        #region Equality

        public override bool Equals(object? obj)
            => _value.Equals(obj);

        public bool Equals(UDouble other)
            => _value.Equals(other._value);

        public override int GetHashCode()
            => _value.GetHashCode();

        public static bool operator ==(UDouble left, UDouble right)
            => left._value == right._value;

        public static bool operator !=(UDouble left, UDouble right)
            => left._value != right._value;

        #endregion Equality
    }
}