using RayVE.LinearAlgebra;
using RayVE.Materials;

namespace RayVE.Surfaces
{
    public interface ISurface
    {
        IMaterial Material { get; }

        Intersections Intersect(Ray ray);

        Vector3D GetNormal(Point3D point);
    }
}