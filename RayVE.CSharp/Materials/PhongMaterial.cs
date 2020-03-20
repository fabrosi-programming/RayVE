using RayVE.LightSources;
using RayVE.LinearAlgebra;
using System;
using static System.Math;

namespace RayVE.Materials
{
    public class PhongMaterial : IMaterial, IEquatable<PhongMaterial>
    {
        private readonly Color _color;

        private readonly UDouble _ambience;

        private readonly UDouble _diffusion;

        private readonly UDouble _specularity;

        private readonly UDouble _shininess;

        public PhongMaterial(Color? color = null, double ambience = 0.1, double diffusion = 0.9, double specularity = 0.9, double shininess = 200.0)
            : this(color ?? new Color(1, 1, 1), new UDouble(ambience), new UDouble(diffusion), new UDouble(specularity), new UDouble(shininess))
        { }

        public PhongMaterial(Color color, UDouble ambience, UDouble diffusion, UDouble specularity, UDouble shininess)
        {
            _color = color;
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

            return left._color == right._color
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
        #endregion

        #region IEquatable<PhongMaterial>
        public bool Equals(PhongMaterial other)
            => this == other;
        #endregion

        #region IMaterial
        public Color Illuminate(Point3D point, Vector3D eyeVector, Vector3D normalVector, ILightSource lightSource)
        {
            if (lightSource is null)
                throw new ArgumentNullException(nameof(lightSource));

            var lightVector = GetLightVector(point, lightSource);
            var illuminationColor = _color * lightSource.Color;
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

            return ambience + diffusion + specularity;
        }

        public Color Illuminate(Intersection intersection, ILightSource lightSource)
            => Illuminate(intersection.Position, intersection.EyeVector, intersection.NormalVector, lightSource);

        private Vector GetLightVector(Point3D point, ILightSource lightSource)
            => new Vector3D(lightSource.Position - point).Normalize();

        private static Vector GetReflectionVector(Vector3D normalVector, Vector lightVector)
            => (-lightVector).Reflect(normalVector);
        #endregion
    }
}