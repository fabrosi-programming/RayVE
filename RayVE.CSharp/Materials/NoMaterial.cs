using RayVE.LightSources;
using RayVE.LinearAlgebra;

namespace RayVE.Materials
{
    public class NoMaterial : IMaterial
    {
        public Color Illuminate(Point3D point, ILightSource lightSource, Vector3D eyeVector, Vector3D reflectionVector)
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