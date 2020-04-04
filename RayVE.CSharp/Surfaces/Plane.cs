using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;
using static RayVE.Constants;

namespace RayVE.Surfaces
{
    public class Plane : SurfaceBase
    {
        public Plane()
            : this(Matrix.Identity(4), new PhongMaterial())
        { }

        public Plane(Matrix transformation)
            : this(transformation, new PhongMaterial())
        { }

        public Plane(IMaterial material)
            : this(Matrix.Identity(4), material)
        { }

        public Plane(Matrix transformation, IMaterial material)
            : base(transformation, material)
        { }

        #region SurfaceBase

        public override ISurface WithMaterial(IMaterial material)
            => new Plane(Transformation, material);

        internal override IEnumerable<double> GetIntersectionsLocal(Ray localizedRay)
            => Abs(localizedRay.Direction.Y) < Epsilon
            ? Array.Empty<double>()
            : new[]
            {
                -localizedRay.Origin.Y / localizedRay.Direction.Y
            };

        internal override Vector3D GetNormalLocal(Point3D localizedPoint)
            => new Vector3D(0, 1, 0);

        #endregion
    }
}
