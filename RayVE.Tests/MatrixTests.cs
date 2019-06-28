using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayVE.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Constructor_With2x2ArrayInput_ExpectCorrectIndexerValues()
        {
            //arrange-act
            var matrix = new Matrix(new[]
            {
                new[] { -3.0d, 5.0d },
                new[] { 1.0d, -2.0d }
            });

            //assert
            Assert.AreEqual(-3.0d, matrix[0, 0]);
            Assert.AreEqual(5.0d, matrix[0, 1]);
            Assert.AreEqual(1.0d, matrix[1, 0]);
            Assert.AreEqual(-2.0d, matrix[1, 1]);
        }

        [TestMethod]
        public void Constructor_With3x3ArrayInput_ExpectCorrectIndexerValues()
        {
            //arrange-act
            var matrix = new Matrix(new[]
            {
                new[] { -3.0d, 5.0d, 0.0d },
                new[] { 1.0d, -2.0d, -7.0d },
                new[] { 0.0d, 1.0d, 1.0d }
            });

            //assert
            Assert.AreEqual(-3.0d, matrix[0, 0]);
            Assert.AreEqual(5.0d, matrix[0, 1]);
            Assert.AreEqual(0.0d, matrix[0, 2]);
            Assert.AreEqual(1.0d, matrix[1, 0]);
            Assert.AreEqual(-2.0d, matrix[1, 1]);
            Assert.AreEqual(-7.0d, matrix[1, 2]);
            Assert.AreEqual(0.0d, matrix[2, 0]);
            Assert.AreEqual(1.0d, matrix[2, 1]);
            Assert.AreEqual(1.0d, matrix[2, 2]);
        }

        [TestMethod]
        public void Constructor_With4x4ArrayInput_ExpectCorrectIndexerValues()
        {
            //arrange-act
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.5d, 6.5d, 7.5d, 8.5d },
                new[] { 9.0d, 10.0d, 11.0d, 12.0d },
                new[] { 13.5d, 14.5d, 15.5d, 16.5d }
            });

            //assert
            Assert.AreEqual(1.0d, matrix[0, 0]);
            Assert.AreEqual(2.0d, matrix[0, 1]);
            Assert.AreEqual(3.0d, matrix[0, 2]);
            Assert.AreEqual(4.0d, matrix[0, 3]);
            Assert.AreEqual(5.5d, matrix[1, 0]);
            Assert.AreEqual(6.5d, matrix[1, 1]);
            Assert.AreEqual(7.5d, matrix[1, 2]);
            Assert.AreEqual(8.5d, matrix[1, 3]);
            Assert.AreEqual(9.0d, matrix[2, 0]);
            Assert.AreEqual(10.0d, matrix[2, 1]);
            Assert.AreEqual(11.0d, matrix[2, 2]);
            Assert.AreEqual(12.0d, matrix[2, 3]);
            Assert.AreEqual(13.5d, matrix[3, 0]);
            Assert.AreEqual(14.5d, matrix[3, 1]);
            Assert.AreEqual(15.5d, matrix[3, 2]);
            Assert.AreEqual(16.5d, matrix[3, 3]);
        }

        [TestMethod]
        public void Constructor_WithValidValues_ExpectCorrectIndexValues()
        {
            //arrange
            var values = new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d }
            };

            //act
            var matrix = new Matrix(values);

            //assert
            Assert.AreEqual(1.0d, matrix[0, 0]);
            Assert.AreEqual(0.0d, matrix[0, 1]);
            Assert.AreEqual(2.0d, matrix[1, 0]);
            Assert.AreEqual(-1.0d, matrix[1, 1]);
        }

        [TestMethod]
        public void Constructor_WithValueSource_ExpectCorrectIndexValues()
        {
            //arrange
            var values = new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d }
            };
            double valueSource(uint i, uint j) => values[i][j];

            //act
            var matrix = new Matrix(2, 2, valueSource);

            //assert
            Assert.AreEqual(1.0d, matrix[0, 0]);
            Assert.AreEqual(0.0d, matrix[0, 1]);
            Assert.AreEqual(2.0d, matrix[1, 0]);
            Assert.AreEqual(-1.0d, matrix[1, 1]);
        }

        [TestMethod]
        public void Constructor_WithNullValues_ExpectArgumentNullException()
        {
            //arrange
            double[][] values = null;

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => new Matrix(values));
        }

        [TestMethod]
        public void SwapRows_WithValidMatrix_ExpectRowsSwapped()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d },
                new double[] { 3.0d, 4.0d }
            });

            //act
            var swapped = matrix.SwapRows(0, 1);

            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 2.0d, -1.0d },
                new double[] { 1.0d, 0.0d },
                new double[] { 3.0d, 4.0d }
            });
            Assert.AreEqual(expected, swapped);
        }

        [TestMethod]
        public void SwapRows_WithOutOfRangeRowIndex1_ExpectArgumentOutOfRangeException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.SwapRows(100, 2));
        }

        [TestMethod]
        public void SwapRows_WithOutOfRangeRowIndex2_ExpectArgumentOutOfRangeException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.SwapRows(2, 100));
        }

        [TestMethod]
        public void ScaleColumn_WithValidColumnIndex_ExpectColumnScaled()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d },
                new double[] { 3.0d, 4.0d }
            });

            //act
            var scaled = matrix.ScaleColumn(0, 0.5d);

            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 0.5d, 0.0d },
                new double[] { 1.0d, -1.0d },
                new double[] { 1.5d, 4.0d }
            });
            Assert.AreEqual(expected, scaled);
        }

        [TestMethod]
        public void ScaleColumn_WithInvalidColumnIndex_ExpectArgumentOutOfRangeException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.ScaleColumn(100, 0.5d));
        }

        [TestMethod]
        public void Transform_WithValidTransform_ExpectTransformedValues()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d },
                new double[] { 3.0d, 4.0d }
            });

            //act
            var transformed = matrix.Transform((i, j) => 2.0d * i + j);

            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d },
                new double[] { 2.0d, 3.0d },
                new double[] { 4.0d, 5.0d }
            });
            Assert.AreEqual(expected, transformed);
        }

        [TestMethod]
        public void Transform_WithValidTransformAndPredicate_ExpectTransformedValues()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 1.0d, 0.0d },
                new double[] { 2.0d, -1.0d },
                new double[] { 3.0d, 4.0d }
            });

            //act
            var transformed = matrix.Transform((i, j) => 2.0d * i + j, (i, j) => i <= j);

            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d },
                new double[] { 2.0d, 3.0d },
                new double[] { 3.0d, 4.0d }
            });
            Assert.AreEqual(expected, transformed);
        }

        [TestMethod]
        public void Transform_WithNullTransform_ExpectArgumentNullException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => matrix.Transform(null));
        }

        [TestMethod]
        public void Transform_WithNullTransformAndNonNullPredicate_ExpectArgumentNullException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => matrix.Transform(null, (i, j) => i <= j));
        }

        [TestMethod]
        public void Transform_WithNullPredicate_ExpectArgumentNullException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => matrix.Transform((i, j) => 2.0d * i + j, null));
        }

        [TestMethod]
        public void Transpose_WithSquareMatrix_ExpectCorrectMatrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -3.0d, 5.0d, 0.0d },
                new[] { 1.0d, -2.0d, -7.0d },
                new[] { 0.0d, 1.0d, 1.0d }
            });

            //act
            var transposed = matrix.Transpose();

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -3.0d, 1.0d, 0.0d },
                new[] { 5.0d, -2.0d, 1.0d },
                new[] { 0.0d, -7.0d, 1.0d }
            }), transposed);
        }

        [TestMethod]
        public void Transpose_WithRectangularMatrix_ExpectCorrectMatrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -3.0d, 5.0d },
                new[] { 1.0d, -2.0d },
                new[] { 0.0d, 1.0d }
            });

            //act
            var transposed = matrix.Transpose();

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -3.0d, 1.0d, 0.0d },
                new[] { 5.0d, -2.0d, 1.0d }
            }), transposed);
        }

        [TestMethod]
        public void Transpose_WithSymmetricMatrix_ExpectNoChange()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -3.0d, 1.0d, 0.0d },
                new[] { 1.0d, -2.0d, -7.0d },
                new[] { 0.0d, -7.0d, 4.0d }
            });

            //act
            var transposed = matrix.Transpose();

            //assert
            Assert.AreEqual(matrix, transposed);
        }

        [TestMethod]
        public void Equals_WithNonMatrixObject_ExpectFalse()
        {
            //arrange
            var matrix = Matrix.Identity(3);
            var other = "asdf";

            //act
            var equal = matrix.Equals(other);

            //assert
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void Equals_WithNullNonMatrixObject_ExpectFalse()
        {
            //arrange
            var matrix = Matrix.Identity(3);
            string other = null;

            //act
            var equal = matrix.Equals(other);

            //assert
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void Equals_WithStructurallyDifferentMatrix_ExpectFalse()
        {
            //arrange
            var matrix = Matrix.Identity(3);
            var other = Matrix.Identity(2);

            //act
            var equal1 = matrix.Equals(other);
            var equal2 = other.Equals(matrix);

            //assert
            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        [TestMethod]
        public void Equals_WithStructurallySameUnequalMatrix_ExpectFalse()
        {
            //arrange
            var matrix = Matrix.Identity(3);
            var other = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d },
                new double[] { 6.0d, 7.0d, 8.0d }
            });

            //act
            var equal1 = matrix.Equals(other);
            var equal2 = other.Equals(matrix);

            //assert
            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        [TestMethod]
        public void Equals_WithSelf_ExpectTrue()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act
            var equal = matrix.Equals(matrix);

            //assert
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void Equals_WithDifferentButEqualMatrix_ExpectTrue()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d },
                new double[] { 6.0d, 7.0d, 8.0d }
            });
            var other = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d },
                new double[] { 6.0d, 7.0d, 8.0d }
            });

            //act
            var equal = matrix.Equals(other);

            //assert
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void EqualsOperator_WithNullLeftMatrix_ExpectFalse()
        {
            //arrange
            Matrix left = null;
            var right = Matrix.Identity(3);

            //act
            var equal = left == right;

            //assert
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void EqualsOperator_WithTwoEqual4x4Matrices_ExpectTrue()
        {
            //arrange
            var matrix1 = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.0d, 6.0d, 7.0d, 8.0d },
                new[] { 9.0d, 8.0d, 7.0d, 6.0d },
                new[] { 5.0d, 4.0d, 3.0d, 2.0d }
            });

            var matrix2 = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.0d, 6.0d, 7.0d, 8.0d },
                new[] { 9.0d, 8.0d, 7.0d, 6.0d },
                new[] { 5.0d, 4.0d, 3.0d, 2.0d }
            });

            //act
            var areEqual = matrix1 == matrix2;

            //assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void EqualsOperator_WithTwoUnequal4x4Matrices_ExpectFalse()
        {
            //arrange
            var matrix1 = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.0d, 6.0d, 7.0d, 8.0d },
                new[] { 9.0d, 8.0d, 7.0d, 6.0d },
                new[] { 5.0d, 4.0d, 3.0d, 2.0d }
            });

            var matrix2 = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.0d, 6.0d, 7.0d, 8.0d },
                new[] { 10.0d, 8.0d, 7.0d, 6.0d },
                new[] { 5.0d, 4.0d, 3.0d, 2.0d }
            });

            //act
            var areEqual = matrix1 == matrix2;

            //assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void MultiplyOperator_WithTwo4x4Matrices_ExpectCorrectValues()
        {
            //arrange
            var matrix1 = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 3.0d, 4.0d },
                new[] { 5.0d, 6.0d, 7.0d, 8.0d },
                new[] { 9.0d, 8.0d, 7.0d, 6.0d },
                new[] { 5.0d, 4.0d, 3.0d, 2.0d }
            });
            
            var matrix2 = new Matrix(new[]
            {
                new[] { -2.0d, 1.0d, 2.0d, 3.0d },
                new[] { 3.0d, 2.0d, 1.0d, -1.0d },
                new[] { 4.0d, 3.0d, 6.0d, 5.0d },
                new[] { 1.0d, 2.0d, 7.0d, 8.0d }
            });

            //act
            var product = matrix1 * matrix2;

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { 20.0d, 22.0d, 50.0d, 48.0d },
                new[] { 44.0d, 54.0d, 114.0d, 108.0d },
                new[] { 40.0d, 58.0d, 110.0d, 102.0d },
                new[] { 16.0d, 26.0d, 46.0d, 42.0d }
            }), product);
        }

        [TestMethod]
        public void MultiplyOperator_WithAnIdentityMatrix_ExpectOriginalMatrix()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d },
                new double[] { 6.0d, 7.0d, 8.0d }
            });
            var identity = Matrix.Identity(3);

            //act
            var product = matrix * identity;

            //assert
            Assert.AreEqual(matrix, product);
        }

        [TestMethod]
        public void MultiplyOperator_WithAZeroMatrix_ExpectOriginalMatrix()
        {
            //arrange
            var matrix = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d },
                new double[] { 6.0d, 7.0d, 8.0d }
            });
            var zero = Matrix.Zero(3, 3);

            //act
            var product = matrix * zero;

            //assert
            Assert.AreEqual(zero, product);
        }

        [TestMethod]
        public void MultiplyOperator_WithRectangularMatrices_ExpectCorrectProduct()
        {
            //arrange
            var left = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d, 2.0d },
                new double[] { 3.0d, 4.0d, 5.0d }
            });
            var right = new Matrix(new double[][]
            {
                new double[] { 1.0d, 2.0d },
                new double[] { 4.0d, 5.0d },
                new double[] { 7.0d, 8.0d }
            });

            //act
            var product = left * right;

            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 18.0d, 21.0d },
                new double[] { 54.0d, 66.0d },
            });
            Assert.AreEqual(expected, product);
        }

        [TestMethod]
        public void MultiplyOperator_WithNullLeftMatrix_ExpectArgumentNullException()
        {
            //arrange
            Matrix left = null;
            var right = Matrix.Identity(3);

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => left * right);
        }

        [TestMethod]
        public void MultiplyOperator_WithNullRightMatrix_ExpectArgumentNullException()
        {
            //arrange
            var left = Matrix.Identity(3);
            Matrix right = null;

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => left * right);
        }
    }
}
