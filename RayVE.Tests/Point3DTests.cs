using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayVE.Tests
{
    [TestClass]
    public class Point3DTests
    {
        [TestMethod]
        public void Constructor_WithXYZCoordinates_ExpectCorrectXYZValues()
        {
            //arrange-act
            var point = new Point3D(4.3d, -4.2d, 3.1d);

            //assert
            Assert.AreEqual(4.3d, point.X);
            Assert.AreEqual(-4.2d, point.Y);
            Assert.AreEqual(3.1d, point.Z);
        }

        [TestMethod]
        public void Constructor_WithPoint_ExpectCorrectXYZValues()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);

            //act
            var point2 = new Point3D(point1);

            //assert
            Assert.AreEqual(4.3d, point2.X);
            Assert.AreEqual(-4.2d, point2.Y);
            Assert.AreEqual(3.1d, point2.Z);
        }

        [TestMethod]
        public void EqualsOperator_WithOtherEqualPoint_ExpectTrue()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.3d, -4.2d, 3.1d);

            //act
            var areEqual = point1 == point2;

            //assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void EqualsOperator_WithOtherUnequalPoint_ExpectFalse()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.2d, -4.1d, 3.0d);

            //act
            var areEqual = point1 == point2;

            //assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void NotEqualsOperator_WithOtherEqualPoint_ExpectFalse()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.3d, -4.2d, 3.1d);

            //act
            var areEqual = point1 != point2;

            //assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void NotEqualsOperator_WithOtherUnequalPoint_ExpectTrue()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.2d, -4.1d, 3.0d);

            //act
            var areEqual = point1 != point2;

            //assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void MinusOperator_WithValidPoints_ExpectCorrectVector()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.2d, -4.1d, 3.0d);

            //act
            var v = point1 - point2;

            //assert
            Assert.AreEqual(new Vector3D(0.1d, -0.1d, 0.1d), v);
        }

        [TestMethod]
        public void MinusOperator_WithEqualPoints_ExpectZeroVector()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(point1);

            //act
            var v = point1 - point2;

            Assert.AreEqual(Vector3D.Zero, v);
        }

        [TestMethod]
        public void NegateOperator_WithNonOriginPoint_ExpectNegatedPoint()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);

            //act
            var point2 = -point1;

            //assert
            Assert.AreEqual(new Point3D(-4.3d, 4.2d, -3.1d), point2);
        }

        [TestMethod]
        public void NegateOperator_WithZeroPoint_ExpectNoChange()
        {
            //arrange
            var point1 = Point3D.Origin;

            //act
            var point2 = -point1;

            //assert
            Assert.AreEqual(point1, point2);
        }

        [TestMethod]
        public void MultiplyOperator_WithNonZeroScalar_ExpectCorrectPoint()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var scalar = 2.0d;

            //act
            var point2 = scalar * point1;

            //assert
            Assert.AreEqual(new Point3D(8.6d, -8.4d, 6.2d), point2);
        }

        [TestMethod]
        public void MultiplyOperator_WithZeroScalar_ExpectOrigin()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var scalar = 0.0d;

            //act
            var point2 = scalar * point1;

            //assert
            Assert.AreEqual(Point3D.Origin, point2);
        }

        [TestMethod]
        public void MultiplyOperator_WithOneScalar_ExpectCorrectPoint()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var scalar = 1.0d;

            //act
            var point2 = scalar * point1;

            //assert
            Assert.AreEqual(point1, point2);
        }

        [TestMethod]
        public void MultiplyOperator_WithOrigin_ExpectOrigin()
        {
            //arrange
            var point1 = Point3D.Origin;
            var scalar = 2.0d;

            //act
            var point2 = scalar * point1;

            //assert
            Assert.AreEqual(point1, point2);
        }

        [TestMethod]
        public void DivideOperator_WithNonZeroScalar_ExpectCorrectPoint()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var scalar = 2.0d;

            //act
            var point2 = point1 / scalar;

            //assert
            Assert.AreEqual(new Point3D(2.15d, -2.1d, 1.55d), point2);
        }

        [TestMethod]
        public void DivideOperator_WithZeroScalar_ExpecPointAtInfinity()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var scalar = 0.0d;

            //act
            var point2 = point1 / scalar;

            //assert
            Assert.AreEqual(new Point3D(double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity), point2);
        }

        [TestMethod]
        public void Equals_WithNonPoint3DObject_ExpectFalse()
        {
            //arrange
            var point = new Point3D(4.3d, -4.2d, 3.1d);
            var obj = new object();

            //act
            var areEqual = point.Equals(obj);

            //assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_WithUnequalPoint_ExpectFalse()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(4.2d, -4.1d, 3.0d);

            //act
            var areEqual = point1.Equals(point2);

            //assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_WithEqualPoint_ExpectTrue()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            var point2 = new Point3D(point1);

            //act
            var areEqual = point1.Equals(point2);

            //assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_WithEqualPointAsObject_ExpectTrue()
        {
            //arrange
            var point1 = new Point3D(4.3d, -4.2d, 3.1d);
            object point2 = new Point3D(point1);

            //act
            var areEqual = point1.Equals(point2);

            //assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Origin_GetValue_ExpectCorrectPoint()
        {
            //arrange-act
            var origin = Point3D.Origin;

            //assert
            Assert.AreEqual(new Point3D(0.0d, 0.0d, 0.0d), origin);
        }
    }
}
