using RayVE.LinearAlgebra;
using RayVE.Materials;
using System.Collections.Generic;

namespace RayVE.Surfaces
{
    public interface ISurface
    {
        IMaterial Material { get; }

        IEnumerable<Intersection> Intersect(Ray ray);

        Vector3D GetNormal(Point3D point);
    }
}