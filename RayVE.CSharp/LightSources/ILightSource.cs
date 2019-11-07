using RayVE.LinearAlgebra;

namespace RayVE.LightSources
{
    public interface ILightSource
    {
        Color Color { get; }

        Point3D Position { get; } // may be specific to point light source
    }
}