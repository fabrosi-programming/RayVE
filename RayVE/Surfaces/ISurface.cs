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
        IEnumerable<Intersection> Intersect(Ray ray);
    }
}