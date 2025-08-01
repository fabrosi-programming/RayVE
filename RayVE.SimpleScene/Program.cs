using RayVE;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Output;
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

            var document = new PPMDocument(canvas, 511);
            var filePath = $"C:\\temp\\RayVE\\SimpleScene\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
            File.WriteAllText(filePath, document.ToString());

            stopwatch.Stop();
            Debug.WriteLine(stopwatch.Elapsed);
        }

        private static IScene GetScene()
            => new Scene(new[]
                {
                    GetFloor(),
                    GetLeftWall(),
                    GetRightWall(),
                    GetLargeSphere(),
                    GetMediumSphere(),
                    GetSmallSphere()
                },
                new[]
                {
                    GetLightSource()
                //    //GetLightSource1(),
                //    //GetLightSource2(),
                //    //GetLightSource3(),
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

        public static ISurface GetLargeSphere()
            => new Sphere(
                Matrix.Translation(new Vector(-0.5, 1, 0.5)),
                new PhongMaterial(
                    PhongMaterial.Default,
                    new SolidPattern(
                        new Color(0.1, 1, 0.5)),
                    diffusion: new UDouble(0.7),
                    specularity: new UDouble(0.3)));

        public static ISurface GetMediumSphere()
            => new Sphere(
                Matrix.Translation(new Vector(1.5, 0.5, -0.5))
                * Matrix.Scale(new Vector(0.5, 0.5, 0.5)),
                new PhongMaterial(
                    PhongMaterial.Default,
                    //new SolidPattern(
                    //    new Color(0.5, 1, 0.1)),
                    new StripePattern(Color.Green, Color.Blue),
                    diffusion: new UDouble(0.7),
                    specularity: new UDouble(0.3)));

        public static ISurface GetSmallSphere()
            => new Sphere(
                Matrix.Translation(new Vector(-1.5, 0.33, -0.75))
                * Matrix.Scale(new Vector(0.33, 0.33, 0.33))
                * Matrix.Rotation(Dimension.Y, Math.PI / 3),
                new PhongMaterial(
                    PhongMaterial.Default,
                    new RingPattern(
                        Color.Cyan,
                        Color.Yellow,
                        Matrix.Scale(new(0.15, 0.15, 0.15))),
                    diffusion: new UDouble(0.7),
                    specularity: new UDouble(0.3)));

        public static IMaterial GetMatteMaterial()
            => new PhongMaterial(
                PhongMaterial.Default,
                //new SolidPattern(
                //    new Color(1, 0.9, 0.9)),
                new StripePattern(
                    new Color(0.9, 0.9, 0.9),
                    new Color(0.95, 0.95, 0.95)),
                specularity: new UDouble(0));

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

        public static IEnumerable<ILightSource> GetLightSourceGrid()
            => from x in Enumerable.Range(-2, 2).Select(i => Convert.ToDouble(i))
               from y in Enumerable.Range(-2, 2).Select(i => Convert.ToDouble(i))
               select new PointLightSource(
                    new Point3D(6 + (x / 4), 6 + (y / 4), -10),
                    new Color(0.035, 0.035, 0.035));

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
