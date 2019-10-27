using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE
{
    public static class Algebra
    {
        public static double Discriminant(double a, double b, double c)
            => System.Math.Pow(b, 2) - (4 * a * c);
    }
}
