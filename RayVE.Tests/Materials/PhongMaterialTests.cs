using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
using RayVE.Surfaces;
using System.Linq;
using static System.Math;

namespace RayVE.Materials.Tests
{
    [TestClass]
    public class PhongMaterialTests
    {
        [TestMethod]
        public void Illuminate_WithLightBehindCamera_ExpectCorrectIlluminatedColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource);

            //assert
            Assert.AreEqual(new Color(1.9, 1.9, 1.9), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithLightBehindCameraAndIlluminatedPointInShadow_ExpectAmbientColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);
            var isInShadow = true;

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource, isInShadow);

            //assert
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithCameraOffsetBy45Degrees_ExpectCorrectIlluminatedColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var sqrt2over2 = Sqrt(2) / 2;
            var eyeVector = new Vector3D(0, sqrt2over2, sqrt2over2);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource);

            //assert
            Assert.AreEqual(new Color(1.0, 1.0, 1.0), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithLightOffsetBy45Degrees_ExpectCorrectIlluminatedColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 10, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource);

            //assert
            Assert.AreEqual(new Color(0.73639610, 0.73639610, 0.73639610), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithLightOffsetBy45DegreesAndEyeVectorParallelToReflectionVector_ExpectCorrectIlluminatedColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var sqrt2over2 = Sqrt(2) / 2;
            var eyeVector = new Vector3D(0, -sqrt2over2, -sqrt2over2);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 10, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource);

            //assert
            Assert.AreEqual(new Color(1.63639610, 1.63639610, 1.63639610), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithLightBehindSurface_ExpectNoIllumination()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var surface = new Sphere();
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, 10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, surface, eyeVector, normalVector, lightSource);

            //assert
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), illuminatedColor);
        }

        [TestMethod]
        public void Illumination_WithStripePattern_ExpectCorrectColors()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new StripePattern(color1, color2);
            var material = new PhongMaterial(pattern, 1, 0, 0, 200);
            var eye = new Vector3D(0, 0, -1);
            var normal = new Vector3D(0, 0, -1);
            var light = new PointLightSource(new Point3D(0, 0, -10), Color.White);
            var illuminatedPoints = new[]
            {
                new Point3D(0.9, 0, 0),
                new Point3D(1.1, 0, 0)
            };
            var surface = new Sphere();

            //act
            var illuminatedColors = illuminatedPoints
                .Select(p => material.Illuminate(p, surface, eye, normal, light))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                Color.Black
            };
            CollectionAssert.AreEqual(expected, illuminatedColors);
        }
    }
}