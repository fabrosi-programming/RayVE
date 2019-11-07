using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LightSources;
using RayVE.LinearAlgebra;
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
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, lightSource, eyeVector, normalVector);

            //assert
            Assert.AreEqual(new Color(1.9, 1.9, 1.9), illuminatedColor);
        }

        [TestMethod]
        public void Illuminate_WithCameraOffsetBy45Degrees_ExpectCorrectIlluminatedColor()
        {
            //arrange
            var color = new Color(1, 1, 1);
            var material = new PhongMaterial(color, 0.1, 0.9, 0.9, 200.0);
            var illuminatedPoint = new Point3D(0, 0, 0);
            var sqrt2over2 = Sqrt(2) / 2;
            var eyeVector = new Vector3D(0, sqrt2over2, sqrt2over2);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, lightSource, eyeVector, normalVector);

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
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 10, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, lightSource, eyeVector, normalVector);

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
            var sqrt2over2 = Sqrt(2) / 2;
            var eyeVector = new Vector3D(0, -sqrt2over2, -sqrt2over2);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 10, -10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, lightSource, eyeVector, normalVector);

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
            var eyeVector = new Vector3D(0, 0, -1);
            var normalVector = new Vector3D(0, 0, -1);
            var lightPosition = new Point3D(0, 0, 10);
            var lightIntensity = new Color(1, 1, 1);
            var lightSource = new PointLightSource(lightPosition, lightIntensity);

            //act
            var illuminatedColor = material.Illuminate(illuminatedPoint, lightSource, eyeVector, normalVector);

            //assert
            Assert.AreEqual(new Color(0.1, 0.1, 0.1), illuminatedColor);
        }
    }
}