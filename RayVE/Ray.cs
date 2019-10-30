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

        public static Ray operator *(Ray ray, Matrix matrix)
        {
            if (matrix.ColumnCount != ray.Origin.Length)
                throw new DimensionMismatchException();

            var transformedOrigin = ray.Origin * matrix;
            var transformedDirection = ray.Direction * matrix;

            return new Ray(new Point3D(transformedOrigin[0], transformedOrigin[1], transformedOrigin[2]),
                           new Vector3D(transformedDirection[0], transformedDirection[1], transformedDirection[2]));
        }

        public static Ray operator *(Matrix matrix, Ray ray)
        {
            if (matrix.ColumnCount != ray.Origin.Length)
                throw new DimensionMismatchException();

            var transformedOrigin = matrix * ray.Origin;
            var transformedDirection = matrix * ray.Direction;

            return new Ray(new Point3D(transformedOrigin[0], transformedOrigin[1], transformedOrigin[2]),
                           new Vector3D(transformedDirection[0], transformedDirection[1], transformedDirection[2]));
        }
    }
}
