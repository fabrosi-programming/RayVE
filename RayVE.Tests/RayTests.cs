using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;

namespace RayVE.Tests
{
    [TestClass]
    public class RayTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectPropertyValues()
        {
            //arrange
            var origin = new Point3D(0, 1, 2);
            var direction = new Vector3D(3, 4, 5);

            //act
            var ray = new Ray(origin, direction);

            //assert
            Assert.AreEqual(origin, ray.Origin);
            Assert.AreEqual(direction, ray.Direction);
        }

        [TestMethod]
        public void Position_WithValidInputs_ExpectCorrectVector()
        {
            //arrange
            var origin = new Point3D(2, 3, 4);
            var direction = new Vector3D(1, 0, 0);
            var ray = new Ray(origin, direction);

            //act
            var position1 = ray.GetPosition(0);
            var position2 = ray.GetPosition(1);
            var position3 = ray.GetPosition(-1);
            var position4 = ray.GetPosition(2.5);

            //assert
            Assert.AreEqual(new Point3D(2, 3, 4), position1);
            Assert.AreEqual(new Point3D(3, 3, 4), position2);
            Assert.AreEqual(new Point3D(1, 3, 4), position3);
            Assert.AreEqual(new Point3D(4.5, 3, 4), position4);
        }

        [TestMethod]
        public void MultiplyOperator_WithTranslationMatrix_ExpectCorrectTransformedRay()
        {
            //arrange
            var ray = new Ray(new Point3D(1, 2, 3), new Vector3D(0, 1, 0));
            var transformation = Matrix.Translation(new Vector(3, 4, 5));

            //act
            var transformed = transformation * ray;

            //assert
            Assert.AreEqual(new Point3D(4, 6, 8), transformed.Origin);
            Assert.AreEqual(new Vector3D(0, 1, 0), transformed.Direction);
        }

        [TestMethod]
        public void MultiplyOperator_WithScalingMatrix_ExpectCorrectTransformedRay()
        {
            //arrange
            var ray = new Ray(new Point3D(1, 2, 3), new Vector3D(0, 1, 0));
            var transformation = Matrix.Scale(new Vector(2, 3, 4));

            //act
            var transformed = transformation * ray;

            //assert
            Assert.AreEqual(new Point3D(2, 6, 12), transformed.Origin);
            Assert.AreEqual(new Vector3D(0, 3, 0), transformed.Direction);
        }
    }
}