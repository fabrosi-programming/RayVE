using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace RayVE.Extensions
{
    public static class DoubleExtensions
    {
        public static double Clamp(this double value, double min, double max)
            => Min(Max(value, min), max);
    }
}
