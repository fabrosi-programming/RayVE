﻿using Functional.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RayVE.LinearAlgebra;
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
            var ray = new Ray(new Point3D(0, 0, -10), new Vector3D(0, 0, 1));

            //act
            var intersection = new Intersection(distance, surface, ray);

            //assert
            Assert.AreEqual(distance, intersection.Distance);
            Assert.AreEqual(surface, intersection.Surface);
            Assert.AreEqual(ray, intersection.Ray);
            Assert.AreEqual(new Point3D(0, 0, -6.5), intersection.Position);
            Assert.AreEqual(new Vector3D(0, 0, -1), intersection.EyeVector);
            Assert.AreEqual(new Vector3D(0, 0, -1), intersection.NormalVector);
        }

        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectOverPosition()
        {
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var surface = new Sphere(Matrix.Translation(new Vector(0, 0, 1)));
            
            //act
            var intersection = new Intersection(5, surface, ray);

            //assert
            Assert.IsTrue(intersection.OverPosition.Z < -Constants.Epsilon / 2);
            Assert.IsTrue(intersection.Position.Z > intersection.OverPosition.Z);
        }

        [TestMethod]
        public void GetHit_WithPositiveDistances_ExpectCorrectIntersection()
        {
            //arrange
            var sphere = new Sphere();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(1, sphere, ray);
            var intersection2 = new Intersection(2, sphere, ray);
            var intersections = new IntersectionCollection(
                new List<Intersection>()
                {
                    intersection1,
                    intersection2
                });

            //act
            var hit = intersections.GetNearestHit();

            //assert
            Assert.AreEqual(intersection1, hit);
        }

        [TestMethod]
        public void GetHit_WithPositiveAndNegativeDistances_ExpectCorrectIntersection_0()
        {
            //arrange
            var sphere = new Sphere();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(-1, sphere, ray);
            var intersection2 = new Intersection(1, sphere, ray);
            var intersections = new IntersectionCollection(
                new List<Intersection>()
                {
                    intersection1,
                    intersection2
                });

            //act
            var hit = intersections.GetNearestHit();

            //assert
            Assert.AreEqual(intersection2, hit);
        }

        [TestMethod]
        public void GetHit_WithPositiveAndNegativeDistances_ExpectCorrectIntersection_1()
        {
            //arrange
            var sphere = new Sphere();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(5, sphere, ray);
            var intersection2 = new Intersection(7, sphere, ray);
            var intersection3 = new Intersection(-3, sphere, ray);
            var intersection4 = new Intersection(2, sphere, ray);
            var intersections = new IntersectionCollection(
                new List<Intersection>()
                {
                    intersection1,
                    intersection2,
                    intersection3,
                    intersection4
                });

            //act
            var hit = intersections.GetNearestHit();

            //assert
            Assert.AreEqual(intersection4, hit);
        }

        [TestMethod]
        public void GetHit_WithNegativeDistances_ExpectNull()
        {
            //arrange
            var sphere = new Sphere();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(-2, sphere, ray);
            var intersection2 = new Intersection(-1, sphere, ray);
            var intersections = new IntersectionCollection(
                new List<Intersection>()
                {
                    intersection1,
                    intersection2
                });

            //act
            var hit = intersections.GetNearestHit();

            //assert
            Assert.AreEqual(Option<Intersection>.None, hit);
        }

        [TestMethod]
        public void Equals_WithEqualIntersection_ExpectTrue()
        {
            //arrange
            var surface = GetSurfaceMock();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(1.5d, surface, ray);
            var intersection2 = new Intersection(1.5d, surface, ray);

            //act-assert
            Assert.IsTrue(intersection1.Equals(intersection2));
            Assert.IsTrue(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WitnUnequalSurfaces_ExpectFalse()
        {
            //arrange
            var surface1 = GetSurfaceMock();
            var surface2 = GetSurfaceMock();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(1.5d, surface1, ray);
            var intersection2 = new Intersection(1.5d, surface2, ray);

            //act-assert
            Assert.IsFalse(intersection1.Equals(intersection2));
            Assert.IsFalse(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WitnUnequalDistances_ExpectFalse()
        {
            //arrange
            var surface = GetSurfaceMock();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(1.5d, surface, ray);
            var intersection2 = new Intersection(2.5d, surface, ray);

            //act-assert
            Assert.IsFalse(intersection1.Equals(intersection2));
            Assert.IsFalse(intersection2.Equals(intersection1));
        }

        [TestMethod]
        public void Equals_WithNullObject_ExpectFalse()
        {
            //arrange
            var surface = GetSurfaceMock();
            var ray = new Ray(new Point3D(0, 0, 1), new Vector3D(0, 0, 1));
            var intersection1 = new Intersection(1.5d, surface, ray);
            object? nullObject = null;

            //act-assert
            Assert.IsFalse(intersection1.Equals(nullObject));
        }

        [TestMethod]
        public void IsInside_WhenIntesectionIsOutsideSurface_ExpectFalse()
        {
            //arrange
            var ray = new Ray(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var surface = new Sphere();

            //act
            var intersection = new Intersection(4, surface, ray);

            //assert
            Assert.IsFalse(intersection.IsInsideSurface);
        }

        [TestMethod]
        public void IsInside_WhenIntesectionIsOutsideSurface_ExpectTrue()
        {
            //arrange
            var ray = new Ray(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));
            var surface = new Sphere();

            //act
            var intersection = new Intersection(4, surface, ray);

            //assert
            Assert.IsTrue(intersection.IsInsideSurface);
        }

        [TestMethod]
        public void IsInside_WhenIntesectionIsOutsideSurface_ExpectInvertedNormal()
        {
            //arrange
            var ray = new Ray(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));
            var surface = new Sphere();

            //act
            var intersection = new Intersection(4, surface, ray);

            //assert
            Assert.AreEqual(new Vector3D(0, 0, -1), intersection.NormalVector);
        }

        private ISurface GetSurfaceMock()
            => Mock.Of<ISurface>(s =>
                s.GetNormal(It.IsAny<Point3D>()) == new Vector3D(0, 0, 1, true));
    }
}