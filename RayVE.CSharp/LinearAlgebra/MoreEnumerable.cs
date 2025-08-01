using System.Collections.Generic;

namespace RayVE.LinearAlgebra
{
    public static class MoreEnumerable
    {
        public static IEnumerable<uint> UIntRange(uint start, uint count)
        {
            for (var i = start; i < start + count; i++)
                yield return i;
        }
    }
}