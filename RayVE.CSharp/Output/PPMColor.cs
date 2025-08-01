using RayVE.Extensions;
using System;
using System.Collections.Generic;

namespace RayVE.Output
{
    public class PPMColor
    {
        private readonly Color _color;

        public PPMColor(Color color) => _color = color;

        public IEnumerable<string> ToPPM(int maxValue)
            => new List<string>()
            {
                $"{GetPPMValue(_color.R, maxValue)}",
                $"{GetPPMValue(_color.G, maxValue)}",
                $"{GetPPMValue(_color.B, maxValue)}"
            };

        private static int GetPPMValue(double rawValue, int maxValue)
            => Convert.ToInt32((rawValue * maxValue).Clamp(0.0d, maxValue));
    }
}