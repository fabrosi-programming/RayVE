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
    public class Scene : IScene
    {
        public IEnumerable<ISurface> Surfaces { get; }

        public IEnumerable<ILightSource> LightSources { get; }

        public Scene(ISurface surface, ILightSource lightSource)
            : this(new[] { surface }, lightSource)
        { }

        public Scene(IEnumerable<ISurface> surfaces, ILightSource lightSource)
            : this(surfaces, new[] { lightSource })
        { }

        public Scene(ISurface surface, IEnumerable<ILightSource> lightSources)
            : this(new[] { surface }, lightSources)
        { }

        public Scene(IEnumerable<ISurface> surfaces, IEnumerable<ILightSource> lightSources)
        {
            Surfaces = surfaces;
            LightSources = lightSources;
        }

        public Intersections Intersect(Ray ray)
            => new Intersections(
                Surfaces.SelectMany(s => s.Intersect(ray)));

        public Color Shade(Intersection intersection)
            => LightSources.Select(l => intersection.Surface.Material.Illuminate(intersection, l, IsInShadow(intersection.Position, l)))
                           .Aggregate((c1, c2) => c1 + c2);

        public Color Shade(Ray ray)
            => Shade(
                Intersect(ray)
                .GetNearestHit()
                .ValueOr(new Intersection(0.0, NullSurface.Instance, ray)));

        public bool IsInShadow(Point3D point, ILightSource lightSource)
        {
            var shadowVector = lightSource.Position - point;
            var distance = shadowVector.Magnitude;
            var ray = new Ray(point, shadowVector.Normalize());
            var intersections = Intersect(ray);
            var nearestHit = intersections.GetNearestHit()
                .ValueOr(new Intersection(Double.PositiveInfinity, NullSurface.Instance, ray));

            if (nearestHit.Distance < distance)
                return true;
            
            return false;
        }

        public static IScene Default
            => new Scene(
                new[]
                {
                    new Sphere(new PhongMaterial(new Color(0.8, 1.0, 0.6), diffusion: 0.7, specularity: 0.2)),
                    new Sphere(Matrix.Scale(new Vector(0.5, 0.5, 0.5)))
                },
                new PointLightSource(new Point3D(-10, 10, -10), new Color(1, 1, 1)));
    }
}