using RayVE.LightSources;
using RayVE.LinearAlgebra;

namespace RayVE.Materials
{
    public interface IMaterial
    {
        Color Illuminate(Point3D point, Vector3D eyeVector, Vector3D reflectionVector, ILightSource lightSource, bool isInShadow = false);

        Color Illuminate(Intersection intersection, ILightSource lightSource);
    }
}