using RayVE.LinearAlgebra;

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