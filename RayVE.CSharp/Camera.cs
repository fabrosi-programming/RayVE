using MoreLinq;
using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayVE.CSharp
{
    public class Camera : ICamera
    {
        public uint Width { get; }

        public uint Height { get; }

        public double FieldOfView { get; }

        public Matrix InverseTransformation { get; }

        private double HalfFieldOfViewWidth { get; }

        private double HalfFieldOfViewHeight { get; }

        public double PixelSize { get; }

        public Camera(uint width, uint height, double fieldOfView)
            : this(width, height, fieldOfView, Matrix.Identity(4))
        { }

        public Camera(uint width, uint height, double fieldOfView, ViewTransformation viewTransformation)
            : this(width, height, fieldOfView, viewTransformation.Matrix)
        { }

        public Camera(uint width, uint height, double fieldOfView, Matrix transformation)
        {
            Width = width;
            Height = height;
            FieldOfView = fieldOfView;
            InverseTransformation = transformation.Inverse;

            var aspectRatio = (double)Width / Height;
            var halfFieldOfView = Math.Tan(FieldOfView / 2);
            HalfFieldOfViewWidth = aspectRatio >= 1
               ? halfFieldOfView
               : halfFieldOfView * aspectRatio;
            HalfFieldOfViewHeight = aspectRatio < 1
               ? halfFieldOfView
               : halfFieldOfView / aspectRatio;

            PixelSize = 2.0 * HalfFieldOfViewWidth / Width;
        }

        public Ray GetRay(uint x, uint y)
        {
            var xOffset = (x + 0.5) * PixelSize;
            var yOffset = (y + 0.5) * PixelSize;

            var worldX = HalfFieldOfViewWidth - xOffset;
            var worldY = HalfFieldOfViewHeight - yOffset;

            var pixel = new Point3D(InverseTransformation * new Point3D(worldX, worldY, -1));
            var origin = new Point3D(InverseTransformation * Point3D.Zero);
            var direction = new Vector3D((pixel - origin).Normalize());

            return new Ray(origin, direction);
        }

        public Canvas Render(IScene scene)
            => new Canvas(Width, Height, (x, y) =>
            {
                var ray = GetRay(x, y);
                return scene.Shade(ray);
            });
    }
}
