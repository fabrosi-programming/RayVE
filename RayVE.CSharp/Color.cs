using System;
using static RayVE.Constants;
using static System.Math;

namespace RayVE
{
    public struct Color : IEquatable<Color>
    {
        public readonly double R { get; }
        public readonly double G { get; }
        public readonly double B { get; }

        public Color(double red, double green, double blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        public Color(Color color)
            : this(color.R, color.G, color.B)
        { }

        public override string ToString()
            => $"R:{R} G:{G} B:{B}";

        #region Operators
        public static Color Add(Color left, Color right)
            => new(left.R + right.R,
                   left.G + right.G,
                   left.B + right.B);

        public static Color operator +(Color left, Color right)
            => Add(left, right);

        public static Color Subtract(Color left, Color right)
            => new(left.R - right.R,
                   left.G - right.G,
                   left.B - right.B);

        public static Color operator -(Color left, Color right)
            => Subtract(left, right);

        public static Color Multiply(Color color, double scalar)
            => new(color.R * scalar, color.G * scalar, color.B * scalar);

        public static Color operator *(Color color, double scalar)
            => Multiply(color, scalar);

        public static Color operator *(Color color, UDouble scalar)
            => color * scalar.AsDouble();

        public static Color operator *(Color left, Color right)
            => new(left.R * right.R, left.G * right.G, left.B * right.B);

        public static bool operator ==(Color left, Color right)
        {
            // no reference equals check because color is a struct
            // no null check because Color is a struct

            if (left.R == right.R
                && left.G == right.G
                && left.B == right.B)
                return true;

            if (Abs(left.R - right.R) < Epsilon
                && Abs(left.G - right.G) < Epsilon
                && Abs(left.B - right.B) < Epsilon)
                return true;

            return false;
        }

        public static bool operator !=(Color left, Color right)
            => !(left == right);

        #endregion Operators

        #region Equality

        public override bool Equals(object? obj)
        {
            if (obj is Color color)
                return Equals(color);

            return false;
        }

        public bool Equals(Color other)
            => this == other;

        public override int GetHashCode()
            => R.GetHashCode()
             + G.GetHashCode()
             + B.GetHashCode();

        #endregion Equality

        #region Fixed Colors

        public static Color Black => new(0, 0, 0);
        public static Color White => new(1, 1, 1);
        public static Color Red => new(1, 0, 0);
        public static Color Green => new(0, 1, 0);
        public static Color Blue => new(0, 0, 1);
        public static Color Cyan => new(0, 1, 1);
        public static Color Magenta => new(1, 0, 1);
        public static Color Yellow => new(1, 1, 0);

        #endregion FixedColors
    }
}