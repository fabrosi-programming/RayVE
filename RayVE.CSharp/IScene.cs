using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System.Collections.Generic;

namespace RayVE.CSharp
{
    public interface IScene
    {
        IEnumerable<ILightSource> LightSources { get; }
        IEnumerable<ISurface> Surfaces { get; }

        Intersections Intersect(Ray ray);
        Color Shade(Intersection intersection);
        Color Shade(Ray ray);
        bool IsInShadow(Point3D point, ILightSource lightSource);
    }
}