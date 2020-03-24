using RayVE.LightSources;
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
    }
}