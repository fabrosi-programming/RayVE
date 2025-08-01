using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Materials
{
    public interface IMaterial
    {
        Color Illuminate(Point3D point, ISurface surface, Vector3D eyeVector, Vector3D reflectionVector, ILightSource lightSource, bool isInShadow = false);

        Color Illuminate(Intersection intersection, ILightSource lightSource, bool isInShadow = false);
    }
}