using Functional.Option;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace RayVE.Extensions
{
    public static class IntersectionExtensions
    {
        public static Option<Intersection> GetHit(this IEnumerable<Intersection> intersections)
            => intersections.Where(i => i.Distance > 0)
                            .MinBy(i => i.Distance)
                            .FirstOrDefault() ?? Option<Intersection>.None;
    }
}