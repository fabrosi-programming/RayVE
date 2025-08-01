using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayVE.Surfaces
{
    public abstract class SurfaceBase : ISurface
    {
        public Matrix Transformation { get; }

        public Matrix InverseTransformation { get; }

        protected Matrix TransposeInverseTransformation { get; }

        public SurfaceBase(Matrix transformation, IMaterial material)
        {
            Transformation = transformation;
            InverseTransformation = Transformation.Inverse;
            TransposeInverseTransformation = Transformation.Inverse.Transpose;
            Material = material;
        }

        #region ISurface
        public IMaterial Material { get; }

        public abstract ISurface WithMaterial(IMaterial material);

        public IntersectionCollection Intersect(Ray ray)
            => new(
                GetIntersections(ray).Select(i => new Intersection(i, this, ray)));

        private IEnumerable<double> GetIntersections(Ray ray)
        {
            var localizedRay = InverseTransformation * ray;
            return GetIntersectionsLocal(localizedRay);
        }

        internal abstract IEnumerable<double> GetIntersectionsLocal(Ray localizedRay);

        public Vector3D GetNormal(Point3D point)
        {
            var localizedPoint = InverseTransformation * point;
            var objectNormal = GetNormalLocal(localizedPoint);
            return (TransposeInverseTransformation * objectNormal).Normalize();
        }

        internal abstract Vector3D GetNormalLocal(Point3D localizedPoint);

        #endregion
    }
}
