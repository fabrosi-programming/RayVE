using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.LightSources
{
    public interface ILightSource
    {
        Color Color { get; }

        Point3D Position { get; } // may be specific to point light source
    }
}
