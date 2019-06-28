using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE
{
    public class Canvas : IEnumerable<Color>
    {
        private readonly Color[,] _pixels;

        public int Width
            => _pixels.GetUpperBound(0) + 1;

        public int Height
            => _pixels.GetUpperBound(1) + 1;

        private IEnumerable<string> GetPPMHeader(int maxValue)
            => new List<string>()
            {
                "P3",
                $"{Width} {Height}",
                $"{maxValue}"
            };

        public Color this[int x, int y]
        {
            get => _pixels[x, y];
            set => _pixels[x, y] = value;
        }

        public Canvas(int width, int height)
        {
            _pixels = new Color[width, height];
        }

        public void Fill(Color color)
        {
            for (var i = 0; i < Width; i++)
                for (var j = 0; j < Height; j++)
                    _pixels[i, j] = color;
        }

        public string ToPPM(int maxValue)
        {
            var rows = new List<string>();
            rows.AddRange(GetPPMHeader(maxValue));

            for (var i = 0; i < Height; i++)
                rows.AddRange(GetRowPPM(i, maxValue));

            return String.Join(Environment.NewLine, rows) + Environment.NewLine;
        }

        private IEnumerable<string> GetRowPPM(int row, int maxValue)
        {
            var rowValues = new List<string>();

            for (var i = 0; i < Width; i++)
                rowValues.AddRange(_pixels[i, row].ToPPM(maxValue));

            var builder = new StringBuilder();
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

        #region IEnumerable
        public IEnumerator<Color> GetEnumerator()
            => _pixels.Cast<Color>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        #endregion
    }
}
