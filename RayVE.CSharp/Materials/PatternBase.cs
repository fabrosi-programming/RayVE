using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Materials
{
    public abstract class PatternBase : IPattern
    {

        public Matrix Transformation { get; }

        public Matrix InverseTransformation { get; }

        public PatternBase(Matrix transformation)
        {
            Transformation = transformation;
            InverseTransformation = transformation.Inverse;
        }

        protected abstract Color ColorAt(Point3D localPoint);

        public Color ColorAt(Point3D point, ISurface surface)
        {
            var surfacePoint = surface.InverseTransformation * point;
            var patternPoint = InverseTransformation * surfacePoint;
            return ColorAt(patternPoint);
        }
    }
}