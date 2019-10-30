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
        public Point3D Origin { get; }

        public Vector3D Direction { get; }

        public Ray(Point3D origin, Vector3D direction)
        {
            if (origin.Length != direction.Length)
                throw new DimensionMismatchException();

            Origin = origin;
            Direction = direction;
        }

        public Vector GetPosition(double distance)
            => Origin + (distance * Direction);

        public Ray Transform(Matrix transformation)
        {
            if (transformation.ColumnCount != Origin.Length)
                throw new DimensionMismatchException();

            var transformedOrigin = transformation * Origin;
            var transformedDirection = transformation * Direction;

            return new Ray(new Point3D(transformedOrigin[0], transformedOrigin[1], transformedOrigin[2]),
                           new Vector3D(transformedDirection[0], transformedDirection[1], transformedDirection[2]));
        }
    }
}
