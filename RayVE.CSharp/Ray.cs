using RayVE.LinearAlgebra;

namespace RayVE
{
    public class Ray
    {
        public Point3D Origin { get; }

        public Vector3D Direction { get; }

        public Ray(Point3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3D GetPosition(double distance)
            => Origin + (distance * Direction);

        #region Operators

        public static Ray Multiply(Ray ray, Matrix matrix)
        {
            if (matrix.ColumnCount != 4)
                throw new DimensionMismatchException();

            var transformedOrigin = ray.Origin * matrix;
            var transformedDirection = ray.Direction * matrix;

            return new Ray(transformedOrigin, transformedDirection);
        }

        public static Ray operator *(Ray ray, Matrix matrix)
            => Multiply(ray, matrix);

        public static Ray Multiply(Matrix matrix, Ray ray)
        {
            if (matrix.ColumnCount != 4)
                throw new DimensionMismatchException();

            var transformedOrigin = matrix * ray.Origin;
            var transformedDirection = matrix * ray.Direction;

            return new Ray(transformedOrigin, transformedDirection);
        }

        public static Ray operator *(Matrix matrix, Ray ray)
            => Multiply(matrix, ray);
        #endregion
}
}