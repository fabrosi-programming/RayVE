using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RayVE;
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
                new Sphere(
                    new PhongMaterial(
                        PhongMaterial.Default,
                        new SolidPattern(
                            new Color(0.8, 1.0, 0.6)),
                        diffusion: new UDouble(0.7),
                        specularity: new UDouble(0.2))),
                new Sphere(
                    Matrix.Scale(
                        new Vector(0.5, 0.5, 0.5)))
            };
            var expectedLightSource = new[]
            {
                new PointLightSource(
                    new Point3D(-10, 10, -10),
                    new Color(1, 1, 1))
            };

            CollectionAssert.AreEquivalent(expectedSurfaces, scene.Surfaces.ToList()); 
            CollectionAssert.AreEquivalent(expectedLightSource, scene.LightSources.ToList());
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

        [TestMethod]
        public void Shade_WithNonIntersectingRay_ExpectBlack()
        {
            //arrange
            var scene = Scene.Default;
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 1, 0));

            //act
            var color = scene.Shade(ray);

            //assert
            Assert.AreEqual(Color.Black, color);
        }

        [TestMethod]
        public void Shade_WithIntersectingRay_ExpectCorrectColor()
        {
            //arrange
            var scene = Scene.Default;
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));

            //act
            var color = scene.Shade(ray);

            //assert
            Assert.AreEqual(new Color(0.38066119308103435, 0.47582649135129296, 0.28549589481077575), color);
        }

        [TestMethod]
        public void Shade_WithRayCastOnInnerSphere_ExpectCorrectColor()
        {
            //arrange
            var outerMaterial = new PhongMaterial(
                PhongMaterial.Default,
                new SolidPattern(
                    new Color(0.8, 1.0, 0.6)),
                ambience: new UDouble(1.0),
                diffusion: new UDouble(0.7),
                specularity: new UDouble(0.2));
            var innerMaterial = new PhongMaterial(
                PhongMaterial.Default,
                ambience: new UDouble(1.0));
            var surfaces = new[]
            {
                Scene.Default
                    .Surfaces
                    .First()
                    .WithMaterial(outerMaterial),
                Scene.Default
                    .Surfaces
                    .Skip(1)
                    .First()
                    .WithMaterial(innerMaterial)
            };
            var scene = new Scene(surfaces, Scene.Default.LightSources);
            var ray = new Ray(new Point3D(0, 0, 0.75), new Vector3D(0, 0, -1));

            //act
            var color = scene.Shade(ray);

            //assert
            Assert.AreEqual(Color.White, color);
        }

        [TestMethod]
        public void Shade_WithIntersectionInShadow_ExpectCorrectColor()
        {
            //arrange
            var lightSources = new[]
            {
                new PointLightSource(new Point3D(0, 0, -10), new Color(1, 1, 1))
            };
            var surfaces = new[]
            {
                new Sphere(),
                new Sphere(Matrix.Translation(new Vector(0, 0, 10)))
            };
            var scene = new Scene(surfaces, lightSources);
            var ray = new Ray(new Point3D(0, 0, 5), new Vector3D(0, 0, 1));
            var intersection = new Intersection(4, surfaces[1], ray);

            //act
            var color = scene.Shade(intersection);

            //assert
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), color);
        }

        [TestMethod]
        public void IsInShadow_WithPointNotInShadow_ExpectFalse()
        {
            //arrange
            var scene = Scene.Default;
            var point = new Point3D(0, 10, 0);
            var lightSource = scene.LightSources.First();

            //act
            var isInShadow = scene.IsInShadow(point, lightSource);

            //assert
            Assert.IsFalse(isInShadow);
        }

        [TestMethod]
        public void IsInShadow_WhenPointIsBehindSphere_ExpectTrue()
        {
            //arrange
            var scene = Scene.Default;
            var point = new Point3D(10, -10, 10);
            var lightSource = scene.LightSources.First();

            //act
            var isInShadow = scene.IsInShadow(point, lightSource);

            //assert
            Assert.IsTrue(isInShadow);
        }

        [TestMethod]
        public void IsInShadow_WhenLightIsBetweenPointAndSphere_ExpectFalse()
        {
            //arrange
            var scene = Scene.Default;
            var point = new Point3D(-20, 20, -20);
            var lightSource = scene.LightSources.First();

            //act
            var isInShadow = scene.IsInShadow(point, lightSource);

            //assert
            Assert.IsFalse(isInShadow);
        }

        [TestMethod]
        public void IsInShadow_WhenPointIsBetweenLightAndSphere_ExpectFalse()
        {
            //arrange
            var scene = Scene.Default;
            var point = new Point3D(-2, 2, -2);
            var lightSource = scene.LightSources.First();

            //act
            var isInShadow = scene.IsInShadow(point, lightSource);

            //assert
            Assert.IsFalse(isInShadow);
        }
    }
}
