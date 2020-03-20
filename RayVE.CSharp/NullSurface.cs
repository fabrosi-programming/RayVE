using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;

namespace RayVE.CSharp
{
    public class NullSurface : ISurface
    {
        #region Singleton
        // TODO: implement Skeet singleton pattern
        private static NullSurface _instance;
        public static NullSurface Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NullSurface();

                return _instance;
            }
        }
        #endregion

        public IMaterial Material
            => new NoMaterial();

        public Intersections Intersect(Ray ray)
            => new Intersections(new[]
            {
                new Intersection(0.0, this, ray)
            });

        public Vector3D GetNormal(Point3D point)
            => new Vector3D(0, 0, 0);

        public ISurface WithMaterial(IMaterial material)
            => this;
    }
}