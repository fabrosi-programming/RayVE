using System;
using System.Collections.Generic;

namespace RayVE.Output
{
    public class PPMDocument
    {
        private readonly Canvas _canvas;
        private readonly int _maxValue;

        private IEnumerable<string> GetPPMHeader(int maxValue)
            => new List<string>()
            {
                "P3",
                $"{_canvas.Width} {_canvas.Height}",
                $"{maxValue}"
            };

        public PPMDocument(Canvas canvas, int maxValue)
        {
            _canvas = canvas ?? new Canvas(0, 0);
            _maxValue = maxValue;
        }

        public override string ToString()
        {
            var rows = new List<string>();
            rows.AddRange(GetPPMHeader(_maxValue));

            for (var i = 0; i < _canvas.Height; i++)
                rows.AddRange(GetRowPPM(i, _maxValue));

            return String.Join(Environment.NewLine, rows) + Environment.NewLine;
        }

        private IEnumerable<string> GetRowPPM(int row, int maxValue)
        {
            var rowValues = new List<string>();

            var pixels = _canvas.GetPixels();

            for (var i = 0; i < _canvas.Width; i++)
                rowValues.AddRange(new PPMColor(pixels[i, row]).ToPPM(maxValue));

            var subRowValues = new List<string>();
            var subRowLength = 0;

            foreach (var value in rowValues)
            {
                if (subRowLength + value.Length + subRowValues.Count > 70)
                {
                    yield return String.Join(" ", subRowValues);
                    subRowValues = new List<string>();
                    subRowLength = 0;
                }

                subRowValues.Add(value);
                subRowLength += value.Length;
            }

            if (subRowValues.Count > 0)
                yield return String.Join(" ", subRowValues);
        }

    }
}