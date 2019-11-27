using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using static System.Math;

namespace RayVE.Surfaces.Tests
{
    [TestClass]
    public class SphereTests
    {
        [TestMethod]
        public void Intersect_WithUnitSphereAndRayThroughCenter_ExpectCorrect2DistinctDistances()
        {
            //arrange
            var origin = new Point3D(0, 0, -5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere();

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(2, intersections.Count);
            Assert.IsTrue(intersections.Contains(new Intersection(4.0d, sphere)));
            Assert.IsTrue(intersections.Contains(new Intersection(6.0d, sphere)));
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndTangentRay_ExpectCorrect2EqualDistances()
        {
            //arrange
            var origin = new Point3D(0, 1, -5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere();

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(2, intersections.Count);
            Assert.AreEqual(new Intersection(5.0d, sphere), intersections[0]);
            Assert.AreEqual(new Intersection(5.0d, sphere), intersections[1]);
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndNonintersectingRay_ExpectCorrectEmptyDistanceCollection()
        {
            //arrange
            var origin = new Point3D(0, 2, -5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere();

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(0, intersections.Count);
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndInteriorOriginatingRay_ExpectPositiveAndNegativeDistances()
        {
            //arrange
            var origin = new Point3D(0, 0, 0);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere();

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(2, intersections.Count);
            Assert.IsTrue(intersections.Contains(new Intersection(-1.0d, sphere)));
            Assert.IsTrue(intersections.Contains(new Intersection(1.0d, sphere)));
        }

        [TestMethod]
        public void Intersect_WithUnitSphereAndRayInFrontOfSphere_ExpectNegativeDistances()
        {
            //arrange
            var origin = new Point3D(0, 0, 5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var sphere = new Sphere();

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(2, intersections.Count);
            Assert.IsTrue(intersections.Contains(new Intersection(-6.0d, sphere)));
            Assert.IsTrue(intersections.Contains(new Intersection(-4.0d, sphere)));
        }

        [TestMethod]
        public void Intersect_WithScaledSphere_ExpectCorrectIntersections()
        {
            //arrange
            var origin = new Point3D(0, 0, -5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var transformation = Matrix.Scale(new Vector(2, 2, 2));
            var sphere = new Sphere(transformation);

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(2, intersections.Count);
            Assert.IsTrue(intersections.Contains(new Intersection(3.0d, sphere)));
            Assert.IsTrue(intersections.Contains(new Intersection(7.0d, sphere)));
        }

        [TestMethod]
        public void Intersect_WithTranslatedSphere_ExpectCorrectIntersections()
        {
            //arrange
            var origin = new Point3D(0, 0, -5);
            var distance = new Vector3D(0, 0, 1);
            var ray = new Ray(origin, distance);
            var transformation = Matrix.Translation(new Vector(5, 0, 0));
            var sphere = new Sphere(transformation);

            //act
            var intersections = sphere.Intersect(ray);

            //assert
            Assert.AreEqual(0, intersections.Count);
        }

        [TestMethod]
        public void GetNormal_WithUnitSphereAtOrigin_ExpectCorrectNormalVector_0()
        {
            //arrange
            var sphere = new Sphere();
            var normalPoint = new Point3D(1, 0, 0);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(1, 0, 0), normal);
        }

        [TestMethod]
        public void GetNormal_WithUnitSphereAtOrigin_ExpectCorrectNormalVector_1()
        {
            //arrange
            var sphere = new Sphere();
            var normalPoint = new Point3D(0, 1, 0);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(0, 1, 0), normal);
        }

        [TestMethod]
        public void GetNormal_WithUnitSphereAtOrigin_ExpectCorrectNormalVector_2()
        {
            //arrange
            var sphere = new Sphere();
            var normalPoint = new Point3D(0, 0, 1);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(0, 0, 1), normal);
        }

        [TestMethod]
        public void GetNormal_WithUnitSphereAtOrigin_ExpectCorrectNormalVector_4()
        {
            //arrange
            var sphere = new Sphere();
            var sqrt3over3 = Sqrt(3) / 3;
            var normalPoint = new Point3D(sqrt3over3, sqrt3over3, sqrt3over3);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(sqrt3over3, sqrt3over3, sqrt3over3), normal);
        }

        [TestMethod]
        public void GetNormal_WithUnitSphereAtOrigin_ExpectNormalizedVector()
        {
            //arrange
            var sphere = new Sphere();
            var sqrt3over3 = Sqrt(3) / 3;
            var normalPoint = new Point3D(sqrt3over3, sqrt3over3, sqrt3over3);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(1, normal.Magnitude);
        }

        [TestMethod]
        public void GetNormal_WithTranslatedSphere_ExpectCorrectNormalVector()
        {
            //arrange
            var transformation = Matrix.Translation(new Vector(0, 1, 0));
            var sphere = new Sphere(transformation);
            var normalPoint = new Point3D(0, 1.70710678, -0.70710678);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(0, 0.70710678, -0.70710678), normal);
        }

        [TestMethod]
        public void GetNormal_WithScaledAndRotatedSphere_ExpectCorrectNormalVector()
        {
            //arrange
            var transformation = Matrix.Scale(new Vector(1, 0.5, 1)) * Matrix.Rotation(Dimension.Z, PI / 5);
            var sphere = new Sphere(transformation);
            var sqrt2over2 = Sqrt(2) / 2;
            var normalPoint = new Point3D(0, sqrt2over2, -sqrt2over2);

            //act
            var normal = sphere.GetNormal(normalPoint);

            //assert
            Assert.AreEqual(new Vector3D(0, 0.97014250, -0.24253563), normal);
        }
    }
}