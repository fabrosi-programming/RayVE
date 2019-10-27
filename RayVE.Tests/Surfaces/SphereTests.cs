using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;

namespace RayVE.Tests.Surfaces
{
    [TestClass]
    public class SphereTests
    {
        [TestMethod]
        public void Intersect_WithUnitSphereAndRayThroughCenter_ExpectCorrect2DistinctDistances()
        {
            //arrange
            var origin = new Vector(0, 0, -5);
            var distance = new Vector(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere(new Vector(0, 0, 0), 1.0d);

            //act
            var intersectionDistances = sphere.Intersect(ray)
                                              .ToList();

            //assert
            Assert.AreEqual(2, intersectionDistances.Count);
            CollectionAssert.Contains(intersectionDistances, new Intersection(4.0d, sphere));
            CollectionAssert.Contains(intersectionDistances, new Intersection(6.0d, sphere));
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndTangentRay_ExpectCorrect2EqualDistances()
        {
            //arrange
            var origin = new Vector(0, 1, -5);
            var distance = new Vector(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere(new Vector(0, 0, 0), 1.0d);

            //act
            var intersectionDistances = sphere.Intersect(ray)
                                              .ToList();

            //assert
            Assert.AreEqual(2, intersectionDistances.Count);
            Assert.AreEqual(new Intersection(5.0d, sphere), intersectionDistances[0]);
            Assert.AreEqual(new Intersection(5.0d, sphere), intersectionDistances[1]);
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndNonintersectingRay_ExpectCorrectEmptyDistanceCollection()
        {
            //arrange
            var origin = new Vector(0, 2, -5);
            var distance = new Vector(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere(new Vector(0, 0, 0), 1.0d);

            //act
            var intersectionDistances = sphere.Intersect(ray)
                                              .ToList();

            //assert
            Assert.AreEqual(0, intersectionDistances.Count);
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndInteriorOriginatingRay_ExpectPositiveAndNegativeDistances()
        {
            //arrange
            var origin = new Vector(0, 0, 0);
            var distance = new Vector(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere(new Vector(0, 0, 0), 1.0d);

            //act
            var intersectionDistances = sphere.Intersect(ray)
                                              .ToList();

            //assert
            Assert.AreEqual(2, intersectionDistances.Count);
            CollectionAssert.Contains(intersectionDistances, new Intersection(-1.0d, sphere));
            CollectionAssert.Contains(intersectionDistances, new Intersection(1.0d, sphere));
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndRayInFrontOfSphere_ExpectNegativeDistances()
        {
            //arrange
            var origin = new Vector(0, 0, 5);
            var distance = new Vector(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere(new Vector(0, 0, 0), 1.0d);

            //act
            var intersectionDistances = sphere.Intersect(ray)
                                              .ToList();

            //assert
            Assert.AreEqual(2, intersectionDistances.Count);
            CollectionAssert.Contains(intersectionDistances, new Intersection(-6.0d, sphere));
            CollectionAssert.Contains(intersectionDistances, new Intersection(-4.0d, sphere));
        }
    }
}
