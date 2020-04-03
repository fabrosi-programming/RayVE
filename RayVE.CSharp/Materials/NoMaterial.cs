using RayVE.LightSources;
using RayVE.LinearAlgebra;

namespace RayVE.Materials
{
    public class NoMaterial : IMaterial
    {
        public Color Illuminate(Point3D point, Vector3D eyeVector, Vector3D reflectionVector, ILightSource lightSource, bool isInShadow = false)
            => Color.Black;

        public Color Illuminate(Intersection intersection, ILightSource lightSource, bool isInShadow = false)
            => Color.Black;

        #region Equals
        public override bool Equals(object? obj)
        {
            if (obj is NoMaterial)
                return true;

            return false;
        }
        #endregion
    }
}