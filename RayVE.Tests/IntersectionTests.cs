using Functional.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RayVE.Extensions;
using RayVE.Surfaces;
using System.Collections.Generic;

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

        [TestMethod]
        public void Equals_WithEqualIntersection_ExpectTrue()
        {
            //arrange
            var surface = Mock.Of<ISurface>();
            var intersection1 = new Intersection(1.5d, surface);
            var intersection2 = new Intersection(1.5d, surface);

            //act-assert
            Assert.IsTrue(intersection1.Equals(intersection2));
            Assert.IsTrue(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WitnUnequalSurfaces_ExpectFalse()
        {
            //arrange
            var surface1 = Mock.Of<ISurface>();
            var surface2 = Mock.Of<ISurface>();
            var intersection1 = new Intersection(1.5d, surface1);
            var intersection2 = new Intersection(1.5d, surface2);

            //act-assert
            Assert.IsFalse(intersection1.Equals(intersection2));
            Assert.IsFalse(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WitnUnequalDistances_ExpectFalse()
        {
            //arrange
            var surface = Mock.Of<ISurface>();
            var intersection1 = new Intersection(1.5d, surface);
            var intersection2 = new Intersection(2.5d, surface);

            //act-assert
            Assert.IsFalse(intersection1.Equals(intersection2));
            Assert.IsFalse(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WithNullObject_ExpectFalse()
        {
            //arrange
            var surface = Mock.Of<ISurface>();
            var intersection1 = new Intersection(1.5d, surface);
            object? nullObject = null;

            //act-assert
            Assert.IsFalse(intersection1.Equals(nullObject));
        }
    }
}