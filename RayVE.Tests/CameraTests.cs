using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE;
using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayVE.Tests
{
    [TestClass]
    public class CameraTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectProperties()
        {
            //arrange
            var width = 160u;
            var height = 120u;
            var fieldOfView = Math.PI / 2;

            //act
            var camera = new Camera(width, height, fieldOfView);

            //assert
            Assert.AreEqual(width, camera.Width);
            Assert.AreEqual(height, camera.Height);
            Assert.AreEqual(fieldOfView, camera.FieldOfView);
            Assert.AreEqual(Matrix.Identity(4), camera.InverseTransformation);
        }

        [TestMethod]
        public void Constructor_WithLandscapeCanvas_ExpectCorrectPixelSize()
        {
            //arrange
            var width = 200u;
            var height = 125u;
            var fieldOfView = Math.PI / 2;
            var camera = new Camera(width, height, fieldOfView);

            //act
            var pixelSize = camera.PixelSize;

            Assert.AreEqual(0.01, pixelSize, Constants.Epsilon);
        }

        [TestMethod]
        public void Constructor_WithPortraitCanvas_ExpectCorrectPixelSize()
        {
            //arrange
            var width = 125u;
            var height = 200u;
            var fieldOfView = Math.PI / 2;
            var camera = new Camera(width, height, fieldOfView);

            //act
            var pixelSize = camera.PixelSize;

            Assert.AreEqual(0.01, pixelSize, Constants.Epsilon);
        }

        [TestMethod]
        public void GetRay_WithCoordinatesForCenterOfCanvas_ExpectCorrectRay()
        {
            //arrange
            var width = 201u;
            var height = 101u;
            var fieldOfView = Math.PI / 2;
            var camera = new Camera(width, height, fieldOfView);

            //act
            var ray = camera.GetRay(100, 50);

            //assert
            Assert.AreEqual(Point3D.Zero, ray.Origin);
            Assert.AreEqual(new Vector3D(0, 0, -1), ray.Direction);
        }

        [TestMethod]
        public void GetRay_WithCoordinatesForCornerOfCanvas_ExpectCorrectRay()
        {
            //arrange
            var width = 201u;
            var height = 101u;
            var fieldOfView = Math.PI / 2;
            var camera = new Camera(width, height, fieldOfView);

            //act
            var ray = camera.GetRay(0, 0);

            //assert
            Assert.AreEqual(Point3D.Zero, ray.Origin);
            Assert.AreEqual(new Vector3D(0.66518642611945078, 0.33259321305972539, -0.66851235825004807), ray.Direction);
        }

        [TestMethod]
        public void GetRay_WithTransformedCamera_ExpectCorrectRay()
        {
            //arrange
            var width = 201u;
            var height = 101u;
            var fieldOfView = Math.PI / 2;
            var transformation = Matrix.Rotation(Dimension.Y, Math.PI / 4) * Matrix.Translation(new Vector(0, -2, 5));
            var camera = new Camera(width, height, fieldOfView, transformation);

            //act
            var ray = camera.GetRay(100, 50);

            //assert
            Assert.AreEqual(new Point3D(0, 2, -5), ray.Origin);
            Assert.AreEqual(new Vector3D(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2), ray.Direction);
        }

        [TestMethod]
        public void Render_WithDefaultScene_ExpectCorrectCenterPixelColor()
        {
            //arrange
            var scene = Scene.Default;
            var viewTransformation = new ViewTransformation(new Point3D(0, 0, -5), new Point3D(0, 0, 0), new Vector3D(0, 1, 0));
            var camera = new Camera(11, 11, Math.PI / 2, viewTransformation);

            //act
            var image = camera.Render(scene);

            //assert
            Assert.AreEqual(new Color(0.38066119308103435, 0.47582649135129296, 0.28549589481077575), image[5, 5]);
        }
    }
}
