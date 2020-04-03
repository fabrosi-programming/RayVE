using RayVE.CSharp;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.IO;

namespace RayVE.SimpleScene
{
    class Program
    {
        public static void Main()
        {
            var scene = GetScene();
            var camera = GetCamera();
            var canvas = camera.Render(scene);

            var filePath = $"C:\\temp\\RayVE\\SimpleScene\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, canvas.ToPPM(255));
        }

        private static IScene GetScene()
            => new Scene(new[]
                {
                    GetFloor(),
                    GetLeftWall(),
                    GetRightWall(),
                    GetLargestSphere(),
                    GetMediumSphere(),
                    GetSmallSphere()
                },
                new[]
                {
                    GetLightSource()
                });

        public static ISurface GetFloor()
            => new Sphere(
                Matrix.Scale(new Vector(10, 0.01, 10)),
                GetMatteMaterial());

        public static ISurface GetLeftWall()
            => new Sphere(
                Matrix.Translation(new Vector(0, 0, 5))
                    * Matrix.Rotation(Dimension.Y, -Math.PI / 4)
                    * Matrix.Rotation(Dimension.X, Math.PI / 2)
                    * Matrix.Scale(new Vector(10, 0.01, 10)),
                GetMatteMaterial());

        public static ISurface GetRightWall()
            => new Sphere(
                Matrix.Translation(new Vector(0, 0, 5))
                    * Matrix.Rotation(Dimension.Y, Math.PI / 4)
                    * Matrix.Rotation(Dimension.X, Math.PI / 2)
                    * Matrix.Scale(new Vector(10, 0.01, 10)),
                GetMatteMaterial());

        public static ISurface GetLargestSphere()
            => new Sphere(
                Matrix.Translation(new Vector(-0.5, 1, 0.5)),
                new PhongMaterial(
                    new Color(0.1, 1, 0.5),
                    diffusion: 0.7,
                    specularity: 0.3));

        public static ISurface GetMediumSphere()
            => new Sphere(
                Matrix.Translation(new Vector(1.5, 0.5, -0.5))
                * Matrix.Scale(new Vector(0.5, 0.5, 0.5)),
                new PhongMaterial(
                    new Color(0.5, 1, 0.1),
                    diffusion: 0.7,
                    specularity: 0.3));

        public static ISurface GetSmallSphere()
            => new Sphere(
                Matrix.Translation(new Vector(-1.5, 0.33, -0.75))
                * Matrix.Scale(new Vector(0.33, 0.33, 0.33)),
                new PhongMaterial(
                    new Color(1, 0.8, 0.1),
                    diffusion: 0.7,
                    specularity: 0.3));

        public static IMaterial GetMatteMaterial()
            => new PhongMaterial(
                    new Color(1, 0.9, 0.9),
                    specularity: 0);

        public static ILightSource GetLightSource()
            => new PointLightSource(
                new Point3D(10, 10, -10),
                new Color(1, 1, 1));

        public static ICamera GetCamera()
            => new Camera(
                1920,
                1080,
                Math.PI / 3,
                new ViewTransformation(
                    new Point3D(0, 1.5, -5),
                    new Point3D(0, 1, 0),
                    new Vector3D(0, 1, 0)));
    }
}
