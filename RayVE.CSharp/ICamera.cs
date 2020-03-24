using RayVE.LinearAlgebra;

namespace RayVE.CSharp
{
    public interface ICamera
    {
        double FieldOfView { get; }
        uint Height { get; }
        Matrix InverseTransformation { get; }
        double PixelSize { get; }
        uint Width { get; }

        Ray GetRay(uint x, uint y);
        Canvas Render(IScene scene);
    }
}