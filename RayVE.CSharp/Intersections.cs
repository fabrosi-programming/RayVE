using Functional.Option;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace RayVE
{
    public class Intersections
    {
        private readonly List<Intersection> _intersections;

        public Intersection this[int i]
            => _intersections[i];

        public int Count
            => _intersections.Count;

        public bool Contains(Intersection intersection)
            => _intersections.Contains(intersection);

        public Intersections(IEnumerable<Intersection> intersections)
            => _intersections = intersections.ToList(); // sacrifice laziness; prevent multiple enumeration

        public Option<Intersection> GetNearestHit()
            => _intersections.Where(i => i.Distance > 0)
                             .MinBy(i => i.Distance)
                             .FirstOrDefault() ?? Option<Intersection>.None;
    }
}