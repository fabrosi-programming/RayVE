using RayVE.LightSources;
using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.Materials
{
    public interface IMaterial
    {
        Color Illuminate(Point3D point, ILightSource lightSource, Vector3D eyeVector, Vector3D reflectionVector);
    }
}
