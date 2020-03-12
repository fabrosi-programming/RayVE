using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayVE.CSharp
{
    public class Scene
    {
        public IEnumerable<ISurface> Surfaces { get; }

        public IEnumerable<ILightSource> LightSources { get; }

        public Scene(IEnumerable<ISurface> surfaces, IEnumerable<ILightSource> lightSources)
        {
            Surfaces = surfaces;
            LightSources = lightSources;
        }

        public Intersections Intersect(Ray ray)
            => new Intersections(
                Surfaces.SelectMany(s => s.Intersect(ray)));

        public static Scene Default
            => new Scene(
                new[]
                {
                    new Sphere(new PhongMaterial(new Color(0.8, 1.0, 0.6), 0, 0.7, 0.2, 0)),
                    new Sphere(Matrix.Scale(new Vector(0.5, 0.5, 0.5)))
                },
                new[]
                {
                    new PointLightSource(new Point3D(-10, 10, -10), new Color(1, 1, 1))
                });
    }
}
