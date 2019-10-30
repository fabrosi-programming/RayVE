using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.Surfaces;
using RayVE.Extensions;
using Functional.Option;
using RayVE.LinearAlgebra;

namespace RayVE.Tests
{
    [TestClass]
    public class IntersectionTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectFieldValues()
        {
            //arrange
            var distance = 3.5;
            var surface = new Sphere();

            //act
            var intersection = new Intersection(distance, surface);

            //assert
            Assert.AreEqual(distance, intersection.Distance);
            Assert.AreEqual(surface, intersection.Surface);
        }

        [TestMethod]
        public void GetHit_WithPositiveDistances_ExpectCorrectIntersection()
        {
            //arrange
            var sphere = new Sphere();
            var intersection1 = new Intersection(1, sphere);
            var intersection2 = new Intersection(2, sphere);
            var intersections = new List<Intersection>()
            {
                intersection1,
                intersection2
            };

            //act
            var hit = intersections.GetHit();

            //assert
            Assert.AreEqual(intersection1, hit);
        }

        [TestMethod]
        public void GetHit_WithPositiveAndNegativeDistances_ExpectCorrectIntersection_0()
        {
            //arrange
            var sphere = new Sphere();
            var intersection1 = new Intersection(-1, sphere);
            var intersection2 = new Intersection(1, sphere);
            var intersections = new List<Intersection>()
            {
                intersection1,
                intersection2
            };

            //act
            var hit = intersections.GetHit();

            //assert
            Assert.AreEqual(intersection2, hit);
        }

        [TestMethod]
        public void GetHit_WithPositiveAndNegativeDistances_ExpectCorrectIntersection_1()
        {
            //arrange
            var sphere = new Sphere();
            var intersection1 = new Intersection(5, sphere);
            var intersection2 = new Intersection(7, sphere);
            var intersection3 = new Intersection(-3, sphere);
            var intersection4 = new Intersection(2, sphere);
            var intersections = new List<Intersection>()
            {
                intersection1,
                intersection2,
                intersection3,
                intersection4
            };

            //act
            var hit = intersections.GetHit();

            //assert
            Assert.AreEqual(intersection4, hit);
        }

        [TestMethod]
        public void GetHit_WithNegativeDistances_ExpectNull()
        {
            //arrange
            var sphere = new Sphere();
            var intersection1 = new Intersection(-2, sphere);
            var intersection2 = new Intersection(-1, sphere);
            var intersections = new List<Intersection>()
            {
                intersection1,
                intersection2
            };

            //act
            var hit = intersections.GetHit();

            //assert
            Assert.AreEqual(Option<Intersection>.None, hit);
        }
    }
}
