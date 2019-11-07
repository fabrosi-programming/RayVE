using static System.Math;

namespace RayVE
{
    public static class Algebra
    {
        public static double Discriminant(double a, double b, double c)
            => Pow(b, 2) - (4 * a * c);
    }
}