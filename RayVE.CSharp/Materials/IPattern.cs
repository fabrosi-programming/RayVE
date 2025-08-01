using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Materials
{
    public interface IPattern
    {
        Matrix Transformation { get; }
        Matrix InverseTransformation { get; }
        Color ColorAt(Point3D point, ISurface surface);
    }
}
