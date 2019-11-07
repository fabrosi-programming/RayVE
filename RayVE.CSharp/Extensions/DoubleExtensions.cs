using RayVE.LinearAlgebra;
using System.Collections.Generic;
using static System.Math;

namespace RayVE.Extensions
{
    public static class DoubleExtensions
    {
        public static double Clamp(this double value, double min, double max)
            => Min(Max(value, min), max);

        public static Vector ToVector(this IEnumerable<double> values)
            => new Vector(values);
    }
}