using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Materials
{
    public class NoMaterial : IMaterial
    {
        public Color Illuminate(Point3D point, ISurface surface, Vector3D eyeVector, Vector3D reflectionVector, ILightSource lightSource, bool isInShadow = false)
            => Color.Black;

        public Color Illuminate(Intersection intersection, ILightSource lightSource, bool isInShadow = false)
            => Color.Black;

        #region Equals
        public override bool Equals(object? obj)
            => obj is NoMaterial;

        public override int GetHashCode() => 0;
        #endregion
    }
}