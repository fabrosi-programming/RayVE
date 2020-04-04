using RayVE;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RayVE.SimpleScene
{
    class Program
    {
        public static void Main()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var scene = GetScene();
            var camera = GetCamera(640, 480);
            var canvas = camera.Render(scene);

            var filePath = $"C:\\temp\\RayVE\\SimpleScene\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, canvas.ToPPM(511));

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.ReadLine();
        }

        private static IScene GetScene()
            => new Scene(new[]
                {
                    GetFloor(),
                    //GetLeftWall(),
                    //GetRightWall(),
                    GetLargestSphere(),
                    GetMediumSphere(),
                    GetSmallSphere()
                },
                new[]
                {
                    GetLightSource()
                    //GetLightSource1(),
                    //GetLightSource2(),
                    //GetLightSource3(),
                });
        //GetLightSourceGrid());

        public static ISurface GetFloor()
            => new Plane(GetMatteMaterial());

        public static ISurface GetLeftWall()
            => new Plane(
                Matrix.Translation(new Vector(0, 0, 5))
                    * Matrix.Rotation(Dimension.Y, -Math.PI / 4)
                    * Matrix.Rotation(Dimension.X, Math.PI / 2),
                GetMatteMaterial());

        public static ISurface GetRightWall()
            => new Plane(
                Matrix.Translation(new Vector(0, 0, 5))
                    * Matrix.Rotation(Dimension.Y, Math.PI / 4)
                    * Matrix.Rotation(Dimension.X, Math.PI / 2),
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
                new Color(0.8, 0.8, 0.8));

        public static ILightSource GetLightSource1()
            => new PointLightSource(
                new Point3D(10, 10, -10),
                new Color(0.6, 0.3, 0.3));

        public static ILightSource GetLightSource2()
            => new PointLightSource(
                new Point3D(0, 10, -10),
                new Color(0.3, 0.6, 0.3));

        public static ILightSource GetLightSource3()
            => new PointLightSource(
                new Point3D(-10, 10, -10),
                new Color(0.3, 0.3, 0.6));

        //public static IEnumerable<ILightSource> GetLightSourceGrid()
        //    => from x in Enumerable.Range(-2, 5).Select(i => Convert.ToDouble(i))
        //       from y in Enumerable.Range(-2, 5).Select(i => Convert.ToDouble(i))
        //       select new PointLightSource(
        //            new Point3D(6 + (x/10), 6 + (y/10), -10),
        //            new Color(0.035, 0.035, 0.035));

        public static ICamera GetCamera(uint x, uint y)
            => new Camera(
                x,
                y,
                Math.PI / 3,
                new ViewTransformation(
                    new Point3D(0, 1.5, -5),
                    new Point3D(0, 1, 0),
                    new Vector3D(0, 1, 0)));
    }
}
