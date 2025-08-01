using Functional.Option;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RayVE
{
    public class IntersectionCollection : IEnumerable<Intersection>
    {
        private readonly List<Intersection> _intersections;

        public Intersection this[int i]
            => _intersections[i];

        public int Count
            => _intersections.Count;

        public bool Contains(Intersection intersection)
            => _intersections.Contains(intersection);

        public IntersectionCollection(IEnumerable<Intersection> intersections)
            => _intersections = intersections.OrderBy(i => i.Distance) // ordering required for reflections, refraction, and constructive geometry
                                             .ToList(); // sacrifice laziness; prevent multiple enumeration

        public Option<Intersection> GetNearestHit()
            => _intersections.Where(i => i.Distance > 0)
                             .MinBy(i => i.Distance)
            ?? Option<Intersection>.None;
        
        #region IEnumerable<Intersection>
        public IEnumerator<Intersection> GetEnumerator()
            => ((IEnumerable<Intersection>)_intersections).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable<Intersection>)_intersections).GetEnumerator();
        #endregion
    }
}