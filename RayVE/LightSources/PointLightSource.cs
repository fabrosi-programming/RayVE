using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.LightSources
{
    public class PointLightSource : ILightSource
    {
        public Point3D Position { get; }

        public Color Color { get; }
        
        public PointLightSource(Point3D position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
