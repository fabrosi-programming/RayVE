using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE
{
    public class Ray
    {
        public Vector Origin { get; }

        public Vector Direction { get; }

        public Ray(Vector origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector PositionAtDistance(double distance)
            => Origin + (distance * Direction);
    }
}
