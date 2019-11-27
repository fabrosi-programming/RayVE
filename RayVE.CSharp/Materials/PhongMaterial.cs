using RayVE.LightSources;
using RayVE.LinearAlgebra;
using System;
using static System.Math;

namespace RayVE.Materials
{
    public class PhongMaterial : IMaterial
    {
        private readonly Color _color;

        private readonly UDouble _ambience;

        private readonly UDouble _diffusion;

        private readonly UDouble _specularity;

        private readonly UDouble _shininess;

        public PhongMaterial(Color color, UDouble ambience, UDouble diffusion, UDouble specularity, UDouble shininess)
        {
            _color = color;
            _ambience = ambience;
            _diffusion = diffusion;
            _specularity = specularity;
            _shininess = shininess;
        }

        public Color Illuminate(Point3D point, ILightSource lightSource, Vector3D eyeVector, Vector3D normalVector)
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
                    var specularFactor = Pow(reflectionDotEye, _shininess);
                    specularity = lightSource.Color * _specularity * specularFactor;
                }
            }

            var ambience = illuminationColor * _ambience;

            return ambience + diffusion + specularity;
        }

        private Vector GetLightVector(Point3D point, ILightSource lightSource)
            => new Vector3D(lightSource.Position - point).Normalize();

        private static Vector GetReflectionVector(Vector3D normalVector, Vector lightVector)
            => (-lightVector).Reflect(normalVector);
    }
}