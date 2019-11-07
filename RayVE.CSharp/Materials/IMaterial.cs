using RayVE.LightSources;
using RayVE.LinearAlgebra;

namespace RayVE.Materials
{
    public interface IMaterial
    {
        Color Illuminate(Point3D point, ILightSource lightSource, Vector3D eyeVector, Vector3D reflectionVector);
    }
}