using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

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
        public Canvas(uint width, uint height, Func<uint, uint, Color> fillFunction, bool parallel = true)
        {
            _pixels = new Color[width, height];

            if (parallel)
                FillParallel(fillFunction);
            else
                FillNonParallel(fillFunction);
        }

        private void FillNonParallel(Func<uint, uint, Color> fillFunction)
        {
            for (uint x = 0; x < Width; x++)
                for (uint y = 0; y < Height; y++)
                    this[x, y] = fillFunction(x, y);
        }

        private void FillParallel(Func<uint, uint, Color> fillFunction)
        {
            Parallel.For(
                0,
                Width,
                //new ParallelOptions()
                //{
                //    MaxDegreeOfParallelism = 8 //Environment.ProcessorCount
                //},
                x =>
                {
                    for (uint y = 0; y < Height; y++)
                        this[(uint)x, y] = fillFunction((uint)x, y);
                });
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

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Multidimensional array does not waste space since all values are needed to represent a rectangular image.")]
        public Color[,] GetPixels() => (Color[,])_pixels.Clone();
    }
}