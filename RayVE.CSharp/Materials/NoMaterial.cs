using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayVE.LightSources;
using RayVE.LinearAlgebra;

namespace RayVE.Materials
{
    public class NoMaterial : IMaterial
    {
        public Color Illuminate(Point3D point, ILightSource lightSource, Vector3D eyeVector, Vector3D reflectionVector)
            => Color.Black;
    }
}
