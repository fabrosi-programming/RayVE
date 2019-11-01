using RayVE.LightSources;
using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace RayVE.Materials
{
    public class PhongMaterial : IMaterial
    {
        private Color _color;

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
            var illuminationColor = _color * lightSource.Color;
            var lightVector = new Vector3D((lightSource.Position - point)).Normalize();
            var ambience = illuminationColor * _ambience;
            var lightDotNormal = lightVector * normalVector;

            var diffusion = new Color(0, 0, 0);
            var specularity = new Color(0, 0, 0);

            if (lightDotNormal > 0) // light source is not behind the surface
            {
                diffusion = illuminationColor * _diffusion * lightDotNormal;

                var reflectionVector = (-lightVector).Reflect(normalVector);
                var reflectionDotEye = reflectionVector * eyeVector;

                if (reflectionDotEye > 0)
                {
                    var specularFactor = Pow(reflectionDotEye, _shininess);
                    specularity = lightSource.Color * _specularity * specularFactor;
                }
            }

            return ambience + diffusion + specularity;
        }
    }
}
