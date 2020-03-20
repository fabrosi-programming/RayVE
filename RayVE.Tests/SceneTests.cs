using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RayVE.CSharp;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayVE.Tests
{
    [TestClass]
    public class SceneTests
    {
        [TestMethod]
        public void Constructor_WithEmptyCollectionInputs_ExpectNoSurfacesAndNoLightSources()
        {
            //arrange
            var surfaces = new List<ISurface>();
            var lightSource = new List<ILightSource>();

            //act
            var scene = new Scene(surfaces, lightSource);

            //assert
            Assert.AreEqual(0, scene.Surfaces.Count());
            Assert.AreEqual(0, scene.LightSources.Count());
        }

        [TestMethod]
        public void Default_NoInputs_ExpectCorrectSurfacesAndLightSource()
        {
            //arrange-act
            var scene = Scene.Default;

            //assert
            var expectedSurfaces = new[]
            {
                new Sphere(new PhongMaterial(new Color(0.8, 1.0, 0.6), diffusion: 0.7, specularity: 0.2)),
                new Sphere(Matrix.Scale(new Vector(0.5, 0.5, 0.5)))
            };
            var expectedLightSource = new[]
            {
                new PointLightSource(new Point3D(-10, 10, -10), new Color(1, 1, 1))
            };

            CollectionAssert.AreEqual(expectedSurfaces, scene.Surfaces.ToList()); 
            CollectionAssert.AreEqual(expectedLightSource, scene.LightSources.ToList());
        }

        [TestMethod]
        public void Intersect_WithDefaultSceneAndValidRay_ExpectCorrectIntersections()
        {
            //arrange
            var scene = Scene.Default;
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));

            //act
            var intersections = scene.Intersect(ray);

            //assert
            Assert.AreEqual(4, intersections.Count());

            var expectedIntersectionDistances = new[]
            {
                4,
                4.5,
                5.5,
                6
            };
            CollectionAssert.AreEqual(expectedIntersectionDistances, intersections.Select(i => i.Distance).ToList());
        }

        [TestMethod]
        public void Shade_WithExternalIntersection_ExpectCorrectColor()
        {
            //arrange
            var scene = Scene.Default;
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var surface = scene.Surfaces.First();
            var intersection = new Intersection(4, surface, ray);

            //act
            var color = scene.Shade(intersection);

            //assert
            Assert.AreEqual(new Color(0.38066119308103435, 0.47582649135129296, 0.28549589481077575), color);
        }

        [TestMethod]
        public void Shade_WithInternalIntersection_ExpectCorrectColor()
        {
            //arrange
            var light = new PointLightSource(new Point3D(0, 0.25, 0), new Color(1, 1, 1));
            var scene = new Scene(Scene.Default.Surfaces, light);
            var ray = new Ray(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));
            var surface = scene.Surfaces.Skip(1).First();
            var intersection = new Intersection(0.5, surface, ray);

            //act
            var color = scene.Shade(intersection);

            //assert
            Assert.AreEqual(new Color(0.9049844720832575, 0.9049844720832575, 0.9049844720832575), color);
        }
    }
}
