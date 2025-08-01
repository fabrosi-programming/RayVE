using RayVE.LinearAlgebra;
using RayVE.Materials;

namespace RayVE.Surfaces
{
    public interface ISurface
    {
        IMaterial Material { get; }

        Matrix InverseTransformation { get; }

        Matrix Transformation { get; }

        IntersectionCollection Intersect(Ray ray);

        Vector3D GetNormal(Point3D point);

        ISurface WithMaterial(IMaterial material);
    }
}