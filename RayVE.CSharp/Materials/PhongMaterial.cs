using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System;
using static System.Math;

namespace RayVE.Materials
{
    public class PhongMaterial : IMaterial, IEquatable<PhongMaterial>
    {
        private readonly IPattern _pattern;

        private readonly UDouble _ambience;

        private readonly UDouble _diffusion;

        private readonly UDouble _specularity;

        private readonly UDouble _shininess;

        public PhongMaterial(Color color, double ambience, double diffusion, double specularity, double shininess)
            : this(color, new UDouble(ambience), new UDouble(diffusion), new UDouble(specularity), new UDouble(shininess))
        { }

        public PhongMaterial(IPattern pattern, double ambience, double diffusion, double specularity, double shininess)
            : this(pattern, new UDouble(ambience), new UDouble(diffusion), new UDouble(specularity), new UDouble(shininess))
        { }

        public PhongMaterial(Color color, UDouble ambience, UDouble diffusion, UDouble specularity, UDouble shininess)
            : this(new SolidPattern(color), ambience, diffusion, specularity, shininess)
        { }

        public PhongMaterial(PhongMaterial source, IPattern? pattern = null, UDouble? ambience = null, UDouble? diffusion = null, UDouble? specularity = null, UDouble? shininess = null)
            : this(
                  pattern ?? source._pattern,
                  ambience ?? source._ambience,
                  diffusion ?? source._diffusion,
                  specularity ?? source._specularity,
                  shininess ?? source._shininess)
        { }

        public PhongMaterial(IPattern pattern, UDouble ambience, UDouble diffusion, UDouble specularity, UDouble shininess)
        {
            _pattern = pattern ?? new SolidPattern(Color.White);
            _ambience = ambience;
            _diffusion = diffusion;
            _specularity = specularity;
            _shininess = shininess;
        }

        #region Operators
        public static bool operator ==(PhongMaterial left, PhongMaterial right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left._pattern.Equals(right._pattern) // no support for custom == operator on interfaces
                && left._ambience == right._ambience
                && left._diffusion == right._diffusion
                && left._specularity == right._specularity
                && left._shininess == right._shininess;
        }

        public static bool operator !=(PhongMaterial left, PhongMaterial right)
            => !(left == right);
        #endregion

        #region Equals
        public override bool Equals(object? obj)
        {
            if (obj is PhongMaterial phongMaterial)
                return Equals(phongMaterial);

            return false;
        }

        public override int GetHashCode() => (_pattern, _ambience, _diffusion, _specularity, _shininess).GetHashCode();
        #endregion

        #region IEquatable<PhongMaterial>
        public bool Equals(PhongMaterial? other)
            => other is not null
            && this == other;
        #endregion

        #region IMaterial
        public Color Illuminate(Point3D point, Color patternColor, Vector3D eyeVector, Vector3D normalVector, ILightSource lightSource, bool isInShadow = false)
        {
            if (lightSource is null)
                throw new ArgumentNullException(nameof(lightSource));

            var lightVector = GetLightVector(point, lightSource);
            var illuminationColor = patternColor * lightSource.Color;
            var lightDotNormal = lightVector * normalVector;

            var diffusion = new Color(0, 0, 0);
            var specularity = new Color(0, 0, 0);

            if (lightDotNormal > 0) // light source is not behind the surface
            {
                diffusion = illuminationColor * _diffusion * lightDotNormal;
                var reflectionVector = GetReflectionVector(normalVector, lightVector);
                var reflectionDotEye = reflectionVector * eyeVector;

                if (reflectionDotEye > 0)
                {
                    var specularFactor = Pow(reflectionDotEye, _shininess.AsDouble());
                    specularity = lightSource.Color * _specularity * specularFactor;
                }
            }

            var ambience = illuminationColor * _ambience;

            if (isInShadow)
                return ambience;

            return ambience + diffusion + specularity;
        }

        public Color Illuminate(Point3D point, ISurface surface, Vector3D eyeVector, Vector3D normalVector, ILightSource lightSource, bool isInShadow = false)
            => Illuminate(point, _pattern.ColorAt(point, surface), eyeVector, normalVector, lightSource, isInShadow);

        public Color Illuminate(Intersection intersection, ILightSource lightSource, bool isInShadow = false)
            => Illuminate(intersection.Position, intersection.Surface, intersection.EyeVector, intersection.NormalVector, lightSource, isInShadow);

        private Vector3D GetLightVector(Point3D point, ILightSource lightSource)
            => (lightSource.Position - point).Normalize();

        private static Vector3D GetReflectionVector(Vector3D normalVector, Vector3D lightVector)
            => (-lightVector).Reflect(normalVector);
        #endregion

        public static PhongMaterial Default
            => new PhongMaterial(
                new SolidPattern(Color.White),
                0.1,
                0.9,
                0.9,
                200);
    }
}