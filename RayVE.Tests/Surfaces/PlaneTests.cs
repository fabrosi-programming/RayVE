using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayVE.Tests.Surfaces
{
    [TestClass]
    public class PlaneTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectConstantNormal()
        {
            //arrange
            var normal = new Vector3D(0, 1, 0);
            var plane = new Plane();

            //act
            var normal1 = plane.GetNormal(new Point3D(0, 0, 0));
            var normal2 = plane.GetNormal(new Point3D(10, 0, -10));
            var normal3 = plane.GetNormal(new Point3D(-5, 0, 150));

            //assert
            var expected = new Vector3D(0, 1, 0);
            Assert.AreEqual(expected, normal1);
            Assert.AreEqual(expected, normal2);
            Assert.AreEqual(expected, normal3);
        }

        [TestMethod]
        public void GetIntersections_WithRayParallelToPlane_ExpectNone()
        {
            //arrange
            var normal = new Vector3D(0, 1, 0);
            var plane = new Plane();
            var ray = new Ray(new Point3D(0, 10, 0), new Vector3D(0, 0, 1));

            //act
            var intersections = plane.Intersect(ray);

            //assert
            Assert.IsFalse(intersections.Any());
        }

        [TestMethod]
        public void GetIntersections_WithRayCoplanarToPlane_ExpectNone()
        {
            //arrange
            var normal = new Vector3D(0, 1, 0);
            var plane = new Plane();
            var ray = new Ray(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));

            //act
            var intersections = plane.Intersect(ray);

            //assert
            Assert.IsFalse(intersections.Any());
        }

        [TestMethod]
        public void GetIntersections_WithRayIntersectingPlaneFromAbove_ExpectCorrectIntersection()
        {
            //arrange
            var normal = new Vector3D(0, 1, 0);
            var plane = new Plane();
            var ray = new Ray(new Point3D(0, 1, 0), new Vector3D(0, -1, 0));

            //act
            var intersections = plane.Intersect(ray);

            //assert
            Assert.AreEqual(1, intersections.Count);
            var expected = new Intersection(1.0, plane, ray);
            Assert.AreEqual(expected, intersections.First());
        }

        [TestMethod]
        public void GetIntersections_WithRayIntersectingPlaneFromBelow_ExpectCorrectIntersection()
        {
            //arrange
            var normal = new Vector3D(0, 1, 0);
            var plane = new Plane();
            var ray = new Ray(new Point3D(0, -1, 0), new Vector3D(0, 1, 0));

            //act
            var intersections = plane.Intersect(ray);

            //assert
            Assert.AreEqual(1, intersections.Count);
            var expected = new Intersection(1.0, plane, ray);
            Assert.AreEqual(expected, intersections.First());
        }
    }
}
