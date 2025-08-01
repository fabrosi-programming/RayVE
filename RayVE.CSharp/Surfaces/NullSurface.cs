using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;

namespace RayVE
{
    public class NullSurface : ISurface
    {
        #region Singleton
        // TODO: implement Skeet singleton pattern
        public static NullSurface Instance { get; } = new();
        #endregion

        public IMaterial Material
            => new NoMaterial();

        public Matrix InverseTransformation => Matrix.Identity(4);

        public Matrix Transformation => Matrix.Identity(4);

        public IntersectionCollection Intersect(Ray ray)
            => new(new[]
            {
                new Intersection(0.0, this, ray)
            });

        public Vector3D GetNormal(Point3D point)
            => new(0, 0, 0);

        public ISurface WithMaterial(IMaterial material)
            => this;
    }
}