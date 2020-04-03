using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static RayVE.Constants;
using static System.Math;

namespace RayVE.LinearAlgebra.Tests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void Constructor_WithXYZCoordinates_ExpectCorrectXYZValues()
        {
            //arrange-act
            var vector = new Vector(4.3d, -4.2d, 3.1d);

            //assert
            Assert.AreEqual(4.3d, vector[0]);
            Assert.AreEqual(-4.2d, vector[1]);
            Assert.AreEqual(3.1d, vector[2]);
        }

        [TestMethod]
        public void Constructor_WithPoint_ExpectCorrectXYZValues()
        {
            //arrange
            var point = new Vector(4.3d, -4.2d, 3.1d);

            //act
            var vector = new Vector(point);

            //assert
            Assert.AreEqual(4.3d, vector[0]);
            Assert.AreEqual(-4.2d, vector[1]);
            Assert.AreEqual(3.1d, vector[2]);
        }

        [TestMethod]
        public void Constructor_WithVector_ExpectCorrectXYZValues()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);

            //act
            var vector2 = new Vector(vector1);

            //assert
            Assert.AreEqual(4.3d, vector2[0]);
            Assert.AreEqual(-4.2d, vector2[1]);
            Assert.AreEqual(3.1d, vector2[2]);
        }

        [TestMethod]
        public void Magnitude_WithValidVector_ExpectCorrectMagnitude()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);

            //act
            var magnitude = vector.Magnitude;

            //assert
            Assert.IsTrue(Abs(6.76313536 - magnitude) < Epsilon);
        }

        [TestMethod]
        public void Magnitude_WithZeroVector_ExpectZero()
        {
            //arrange
            var vector = Vector.Zeros(3);

            //act
            var magnitude = vector.Magnitude;

            //assert
            Assert.AreEqual(0.0d, magnitude);
        }

        [TestMethod]
        public void Normalize_WithValidVector_ExpectCorrectVector()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);

            //act
            var normalized = vector.Normalize();

            //assert
            Assert.IsTrue(Abs(0.63579978 - normalized[0]) < Epsilon);
            Assert.IsTrue(Abs(-0.62101374 - normalized[1]) < Epsilon);
            Assert.IsTrue(Abs(0.45836729 - normalized[2]) < Epsilon);
        }

        [TestMethod]
        public void Normalize_WithZeroVector_ExpectNaNVector()
        {
            //arrange
            var vector = Vector.Zeros(3);

            //act
            var normalized = vector.Normalize();

            //assert
            Assert.AreEqual(new Vector(Double.NaN, Double.NaN, Double.NaN), normalized);
        }

        [TestMethod]
        public void Normalize_WithUnitVector_ExpectNoChange()
        {
            //arrange
            var vector = new Vector(1.0d, 0.0d, 0.0d);

            //act
            var normalized = vector.Normalize();

            //assert
            Assert.AreEqual(vector, normalized);
        }

        [TestMethod]
        public void PlusOperator_WithNonZeroVectors_ExpectCorrectVector()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(1.2d, 3.4d, -5.6d);

            //act
            var sum1 = vector1 + vector2;
            var sum2 = vector2 + vector1;

            //assert
            Assert.AreEqual(new Vector(5.5d, -0.8d, -2.5d), sum1);
            Assert.AreEqual(new Vector(5.5d, -0.8d, -2.5d), sum2);
        }

        [TestMethod]
        public void PlusOperator_WithOneZeroVector_ExpectNoChange()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = Vector.Zeros(3);

            //act
            var sum = vector1 + vector2;

            //assert
            Assert.AreEqual(vector1, sum);
        }

        [TestMethod]
        public void PlusOperator_WithVectorAndPoint_ExpectCorrectPoint()
        {
            //arrange
            var point = new Vector(4.3d, -4.2d, 3.1d);
            var vector = new Vector(1.2d, 3.4d, -5.6d);

            //act
            var sum = point + vector;

            //assert
            Assert.AreEqual(new Vector(5.5d, -0.8d, -2.5d), sum);
        }

        [TestMethod]
        public void PlusOperator_WithZeroVector_ExpectOriginalPoint()
        {
            //arrange
            var point = new Vector(4.3d, -4.2d, 3.1d);
            var vector = Vector.Zeros(3);

            //act
            var sum = point + vector;

            //assert
            Assert.AreEqual(point, sum);
        }

        [TestMethod]
        public void MinusOperator_WithNonZeroVectors_ExpectCorrectVector()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(1.2d, 3.4d, -5.6d);

            //act
            var difference1 = vector1 - vector2;
            var difference2 = vector2 - vector1;

            //assert
            Assert.AreEqual(new Vector(3.1d, -7.6d, 8.7d), difference1);
            Assert.AreEqual(new Vector(-3.1d, 7.6d, -8.7d), difference2);
        }

        [TestMethod]
        public void MinusOperator_WithOneZeroVector_ExpectNoChange()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = Vector.Zeros(3);

            //act
            var sum = vector1 - vector2;

            //assert
            Assert.AreEqual(vector1, sum);
        }

        [TestMethod]
        public void MinusOperator_WithVectorAndPoint_ExpectCorrectPoint()
        {
            //arrange
            var point = new Vector(4.3d, -4.2d, 3.1d);
            var vector = new Vector(1.2d, 3.4d, -5.6d);

            //act
            var sum = point - vector;

            //assert
            Assert.AreEqual(new Vector(3.1d, -7.6d, 8.7d), sum);
        }

        [TestMethod]
        public void MinusOperator_WithZeroVector_ExpectOriginalPoint()
        {
            //arrange
            var point = new Vector(4.3d, -4.2d, 3.1d);
            var vector = Vector.Zeros(3);

            //act
            var sum = point - vector;

            //assert
            Assert.AreEqual(point, sum);
        }

        [TestMethod]
        public void MultiplyOperator_WithNonZeroVectors_ExpectCorrectProduct()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(1.2d, 3.4d, -5.6d);

            //act
            var product1 = vector1 * vector2;
            var product2 = vector2 * vector1;

            //assert
            Assert.AreEqual(-26.48, product1);
            Assert.AreEqual(-26.48, product2);
        }

        [TestMethod]
        public void MultiplyOperator_WithOneZeroVector_ExpectZero()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = Vector.Zeros(3);

            //act
            var product = vector1 * vector2;

            //assert
            Assert.AreEqual(0.0d, product);
        }

        [TestMethod]
        public void MultiplyOperator_WithPerpendicularVectors_ExpectZero()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(0.11d, 0.12d, 0.01d);

            //act
            var product = vector1 * vector2;

            //assert
            Assert.IsTrue((0.0d - product) < Epsilon);
        }

        [TestMethod]
        public void MultiplyOperator_WithScalar_ExpectCorrectVector()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);
            var scalar = 2.0d;

            //act
            var scaled = scalar * vector;

            //assert
            Assert.AreEqual(new Vector(8.6d, -8.4d, 6.2d), scaled);
        }

        [TestMethod]
        public void MultiplyOperator_WithOneScalar_ExpectNoChange()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);
            var scalar = 1.0d;

            //act
            var scaled = scalar * vector;

            //assert
            Assert.AreEqual(vector, scaled);
        }

        [TestMethod]
        public void MultiplyOperator_WithZeroScalar_ExpectZeroVector()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);
            var scalar = 0.0d;

            //act
            var scaled = scalar * vector;

            //assert
            Assert.AreEqual(Vector.Zeros(3), scaled);
        }

        [TestMethod]
        public void MultiplyOperator_WithLeftMatrix_ExpectCorrectVector()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 2.0d, 4.0d, 4.0d, 2.0d },
                new[] { 8.0d, 6.0d, 4.0d, 1.0d },
                new[] { 0.0d, 0.0d, 0.0d, 1.0d }
            });
            var vector = new Vector(1, 2, 3, 1);

            //act
            var product = matrix * vector;

            //assert
            Assert.AreEqual(new Vector(18, 24, 33, 1), product);
        }

        [TestMethod]
        public void MultiplyOperator_WithRightMatrix_ExpectCorrectVector()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 2.0d, 4.0d, 4.0d, 2.0d },
                new[] { 8.0d, 6.0d, 4.0d, 1.0d },
                new[] { 0.0d, 0.0d, 0.0d, 1.0d }
            });
            var vector = new Vector(1, 2, 3, 1);

            //act
            var product = vector * matrix;

            //assert
            Assert.AreEqual(new Vector(29, 28, 23, 12), product);
        }

        [TestMethod]
        public void DivideOperator_WithOneScalar_ExpectNoChange()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);
            var scalar = 1.0d;

            //act
            var scaled = vector / scalar;

            //assert
            Assert.AreEqual(vector, scaled);
        }

        [TestMethod]
        public void DivideOperator_WithZeroScalar_ExpectInfinityVector()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);
            var scalar = 0.0d;

            //act
            var scaled = vector / scalar;

            //assert
            Assert.AreEqual(new Vector(Double.PositiveInfinity, Double.NegativeInfinity, Double.PositiveInfinity), scaled);
        }

        [TestMethod]
        public void NegateOperator_WithNonZeroVector_ExpectCorrectVector()
        {
            //arrange
            var vector = new Vector(4.3d, -4.2d, 3.1d);

            //act
            var negated = -vector;

            //assert
            Assert.AreEqual(new Vector(-4.3d, 4.2d, -3.1d), negated);
        }

        [TestMethod]
        public void NegateOperator_WithZeroVector_ExpectZeroVector()
        {
            //arrange
            var vector = Vector.Zeros(3);

            //act
            var negated = -vector;

            //assert
            Assert.AreEqual(vector, negated);
        }

        [TestMethod]
        public void EqualsOperator_WithEqualVectors_ExpectTrue()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.3d, -4.2d, 3.1d);

            //act-assert
            Assert.IsTrue(vector1 == vector2);
            Assert.IsTrue(vector2 == vector1);
        }

        [TestMethod]
        public void EqualsOperator_WithUnequalVectors_ExpectFalse()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.2d, -4.1d, 3.0d);

            //act-assert
            Assert.IsFalse(vector1 == vector2);
            Assert.IsFalse(vector2 == vector1);
        }

        [TestMethod]
        public void NotEqualsOperator_WithEqualVectors_ExpectFalse()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.3d, -4.2d, 3.1d);

            //act-assert
            Assert.IsFalse(vector1 != vector2);
            Assert.IsFalse(vector2 != vector1);
        }

        [TestMethod]
        public void NotEqualsOperator_WithUnequalVectors_ExpectTrue()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.2d, -4.1d, 3.0d);

            //act-assert
            Assert.IsTrue(vector1 != vector2);
            Assert.IsTrue(vector2 != vector1);
        }

        [TestMethod]
        public void Equals_WithNonVectorObject_ExpectFalse()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var obj = new object();

            //act-assert
            Assert.IsFalse(vector1.Equals(obj));
        }

        [TestMethod]
        public void Equals_WithVectorCastAsObject_ExpectTrue()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            object vector2 = new Vector(4.3d, -4.2d, 3.1d);

            //act-assert
            Assert.IsTrue(vector1.Equals(vector2));
        }

        [TestMethod]
        public void Equals_WithEqualVectors_ExpectTrue()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.3d, -4.2d, 3.1d);

            //act-assert
            Assert.IsTrue(vector1.Equals(vector2));
            Assert.IsTrue(vector2.Equals(vector1));
        }

        [TestMethod]
        public void Equals_WithUnequalVectors_ExpectFalse()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            var vector2 = new Vector(4.2d, -4.1d, 3.0d);

            //act-assert
            Assert.IsFalse(vector1.Equals(vector2));
            Assert.IsFalse(vector2.Equals(vector1));
        }

        [TestMethod]
        public void Equals_WithNullObject_ExpectFalse()
        {
            //arrange
            var vector1 = new Vector(4.3d, -4.2d, 3.1d);
            object? nullObject = null;

            //act-assert
            Assert.IsFalse(vector1.Equals(nullObject));
        }

        [TestMethod]
        public void Zero_GetValue_ExpectCorrectVector()
        {
            //arrange-act
            var vector = Vector.Zeros(3);

            //assert
            Assert.AreEqual(new Vector(0.0d, 0.0d, 0.0d), vector);
        }

        [TestMethod]
        public void Translate_WithVectorValues_ExpectTranslatesToCorrectVector()
        {
            //arrange
            var vector = new Vector(5.0d, -3.0d, 2.0d);
            var point = new Point3D(-3.0d, 4.0d, 5.0d);

            //act
            var translated = point.Translate(vector);

            //assert
            Assert.AreEqual(new Point3D(2.0d, 1.0d, 7.0d), translated);
        }

        [TestMethod]
        public void Scale_MultipliedByVector_ExpectCorrectNewPoint()
        {
            //arrange
            var scalars = new Vector3D(2.0d, 3.0d, 4.0d);
            var point = new Vector3D(-4.0d, 6.0d, 8.0d);

            //act
            var newPoint = point.Scale(scalars);

            //assert
            Assert.AreEqual(new Vector3D(-8.0d, 18.0d, 32.0d), newPoint);
        }

        [TestMethod]
        public void Rotate_WithHalfQuarterRotationAboutXAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var dimension = Dimension.X;
            var angle = PI / 4;
            var point = new Point3D(0, 1, 0);

            //act
            var rotatedPoint = point.Rotate(dimension, angle);

            //assert
            Assert.AreEqual(new Point3D(0, Sqrt(2) / 2, Sqrt(2) / 2), rotatedPoint);
        }

        [TestMethod]
        public void TranslateScaleRotate_WithChainedOperations_ExpectCorrectResultPoint()
        {
            //arrange
            var point = new Point3D(1, 0, 1);

            //act
            var transformedPoint = point.Rotate(Dimension.X, PI / 2)
                                        .Scale(new Vector(5, 5, 5))
                                        .Translate(new Vector(10, 5, 7));

            //assert
            Assert.AreEqual(new Point3D(15, 0, 7), transformedPoint);
        }

        [TestMethod]
        public void Reflect_WithNonZeroNormalVector_ExpectCorrectReflectedVector_0()
        {
            //arrange
            var vector = new Vector(1, -1, 0);
            var normal = new Vector(0, 1, 0);

            //act
            var reflected = vector.Reflect(normal);

            //assert
            Assert.AreEqual(new Vector(1, 1, 0), reflected);
        }

        [TestMethod]
        public void Reflect_WithNonZeroNormalVector_ExpectCorrectReflectedVector_1()
        {
            //arrange
            var vector = new Vector(0, -1, 0);
            var sqrt2over2 = Sqrt(2) / 2;
            var normal = new Vector(sqrt2over2, sqrt2over2, 0);

            //act
            var reflected = vector.Reflect(normal);

            //assert
            Assert.AreEqual(new Vector(1, 0, 0), reflected);
        }
    }
}