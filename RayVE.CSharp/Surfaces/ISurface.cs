using RayVE.LinearAlgebra;
using RayVE.Materials;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.Surfaces
{
    public interface ISurface
    {
        IMaterial Material { get; }

        IEnumerable<Intersection> Intersect(Ray ray);

        Vector3D GetNormal(Point3D point);
    }
}