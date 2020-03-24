using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RayVE
{
    public class Canvas
    {
        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Multidimensional array does not waste space since all values are needed to represent a rectangular image.")]
        private readonly Color[,] _pixels;

        public uint Width
            => Convert.ToUInt32(_pixels.GetUpperBound(0) + 1);

        public uint Height
            => Convert.ToUInt32(_pixels.GetUpperBound(1) + 1);

        private IEnumerable<string> GetPPMHeader(int maxValue)
            => new List<string>()
            {
                "P3",
                $"{Width} {Height}",
                $"{maxValue}"
            };

        public Color this[uint x, uint y]
        {
            get
            {
                if (ContainsPoint(x, y))
                    return _pixels[x, y];

                return Color.Black;
            }
            set
            {
                if (ContainsPoint(x, y))
                    _pixels[x, y] = value;
            }
        }

        public Color this[int x, int y]
        {
            get => this[Convert.ToUInt32(x), Convert.ToUInt32(y)];
            set => this[Convert.ToUInt32(x), Convert.ToUInt32(y)] = value;
        }

        public Canvas(int width, int height)
            : this(Convert.ToUInt32(width), Convert.ToUInt32(height))
        { }

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Multidimensional array does not waste space since all values are needed to represent a rectangular image.")]
        public Canvas(uint width, uint height)
            => _pixels = new Color[width, height];

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Multidimensional array does not waste space since all values are needed to represent a rectangular image.")]
        public Canvas(uint width, uint height, Func<uint, uint, Color> fillFunction)
        {
            _pixels = new Color[width, height];

            for (uint x = 0; x < Width; x++)
                for (uint y = 0; y < Height; y++)
                {
                    this[x, y] = fillFunction(x, y);
                }
        }

        public void Fill(Color color)
        {
            for (var i = 0; i < Width; i++)
                for (var j = 0; j < Height; j++)
                    _pixels[i, j] = color;
        }

        public bool ContainsPoint(uint x, uint y)
            => x >= 0 && x < Width
            && y >= 0 && y < Height;

        public bool ContainsPoint(int x, int y)
            => ContainsPoint(Convert.ToUInt32(x), Convert.ToUInt32(y));

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