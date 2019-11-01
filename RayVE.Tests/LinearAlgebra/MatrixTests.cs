using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using static System.Math;

namespace RayVE.LinearAlgebra.Tests
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
            Func<uint, uint, double> valueSource = (i, j) => values[i][j];
#if CSHARP
            //act
            var matrix = new Matrix(2, 2, valueSource);
#endif
#if FSHARP
            var fSharpFunc = valueSource.ToFSharpFunc();

            //act
            var matrix = new Matrix(2, 2, fSharpFunc);
#endif

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
#if CSHARP
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.SwapRows(100, 2));
#elif FSHARP
            Assert.ThrowsException<IndexOutOfRangeException>(() => matrix.SwapRows(100, 2));
#endif
        }

        [TestMethod]
        public void SwapRows_WithOutOfRangeRowIndex2_ExpectArgumentOutOfRangeException()
        {
            //arrange
            var matrix = Matrix.Identity(3);

            //act-assert
#if CSHARP
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.SwapRows(2, 100));
#elif FSHARP
            Assert.ThrowsException<IndexOutOfRangeException>(() => matrix.SwapRows(2, 100));
#endif
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
            Func<uint, uint, double> transform = (i, j) => 2.0d * i + j;

            //act
#if CSHARP
            var transformed = matrix.Transform(transform);
#endif
#if FSHARP
            var transformed = matrix.Transform(transform.ToFSharpFunc());
#endif
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
            Func<uint, uint, double> transform = (i, j) => 2.0d * i + j;
            Func<uint, uint, bool> predicate = (i, j) => i <= j;

            //act
#if CSHARP
            var transformed = matrix.Transform(transform, predicate);
#endif
#if FSHARP
            var transformed = matrix.Transform(transform.ToFSharpFunc(), predicate.ToFSharpFunc());
#endif
            //assert
            var expected = new Matrix(new double[][]
            {
                new double[] { 0.0d, 1.0d },
                new double[] { 2.0d, 3.0d },
                new double[] { 3.0d, 4.0d }
            });
            Assert.AreEqual(expected, transformed);
        }

#if CSHARP
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
            Func<uint, uint, bool> predicate = (i, j) => i <= j;

            //act-assert
            Assert.ThrowsException<ArgumentNullException>(() => matrix.Transform(null, predicate));
        }

        [TestMethod]
        public void Transform_WithNullPredicate_ExpectArgumentNullException()
        {
            //arrange
            var matrix = Matrix.Identity(3);
            Func<uint, uint, double> transform = (i, j) => 2.0d * i + j;

            //act-assert

            Assert.ThrowsException<ArgumentNullException>(() => matrix.Transform(transform, null));
        }
#endif

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
            var transposed = matrix.Transpose;

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
            var transposed = matrix.Transpose;

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
            var transposed = matrix.Transpose;

            //assert
            Assert.AreEqual(matrix, transposed);
        }

        [TestMethod]
        public void Determinant_Wit2x2Matrix_ExpectCorrectValue()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 5.0d },
                new[] {-3.0d, 2.0d }
            });

            //act
            var determinant = matrix.Determinant;

            //assert
            Assert.AreEqual(17.0d, determinant);
        }
        
        [TestMethod]
        public void Determinant_With3x3Matrix_ExpectCorrectValue()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 6.0d },
                new[] { -5.0d, 8.0d, -4.0d },
                new[] { 2.0d, 6.0d, 4.0d }
            });

            //act
            var determinant = matrix.Determinant;

            //assert
            Assert.AreEqual(-196.0d, determinant);
        }

        [TestMethod]
        public void Determinant_With4x4Matrix_ExpectCorrectValue()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -2.0d, -8.0d, 3.0d, 5.0d },
                new[] { -3.0d, 1.0d, 7.0d, 3.0d },
                new[] { 1.0d, 2.0d, -9.0d, 6.0d },
                new[] { -6.0d, 7.0d, 7.0d, -9.0d }
            });

            //act
            var determinant = matrix.Determinant;

            //assert
            Assert.AreEqual(-4071.0d, determinant);
        }

        [TestMethod]
        public void GetSubMatrix_With3x3Matrix_ExpectCorrect2x2Matrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 5.0d, 0.0d },
                new[] { -3.0d, 2.0d, 7.0d },
                new[] { 0.0d, 6.0d, -3.0d }
            });

            //act
            var subMatrix = matrix.GetSubMatrix(0, 2);

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -3.0d, 2.0d },
                new[] { 0.0d, 6.0d }
            }), subMatrix);
        }

        [TestMethod]
        public void GetSubMatrix_With4x4Matrix_ExpectCorrect3x3Matrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -6.0d, 1.0d, 1.0d, 6.0d },
                new[] { -8.0d, 5.0d, 8.0d, 6.0d },
                new[] { -1.0d, 0.0d, 8.0d, 2.0d },
                new[] { -7.0d, 1.0d, -1.0d, 1.0d }
            });

            //act
            var subMatrix = matrix.GetSubMatrix(2, 1);

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -6.0d, 1.0d, 6.0d },
                new[] { -8.0d, 8.0d, 6.0d },
                new[] { -7.0d, -1.0d, 1.0d }
            }), subMatrix);
        }

        [TestMethod]
        public void GetMinor_With3x3Matrix_ExpectCorrectValue()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 3.0d, 5.0d, 0.0d },
                new[] { 2.0d, -1.0d, -7.0d },
                new[] { 6.0d, -1.0d, 5.0d }
            });

            //act
            var minor = matrix.GetMinor(1, 0);

            //assert
            Assert.AreEqual(25.0d, minor);
        }

        [TestMethod]
        public void GetCofactor_With3x3Matrix_ExpectCorrectValues_1()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 3.0d, 5.0d, 0.0d },
                new[] { 2.0d, -1.0d, -7.0d },
                new[] { 6.0d, -1.0d, 5.0d }
            });

            //act
            var cofactor1 = matrix.GetCofactor(0, 0);
            var cofactor2 = matrix.GetCofactor(1, 0);

            //assert
            Assert.AreEqual(-12.0d, cofactor1);
            Assert.AreEqual(-25.0d, cofactor2);
        }

        [TestMethod]
        public void GetCofactor_With3x3Matrix_ExpectCorrectValues_2()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 1.0d, 2.0d, 6.0d },
                new[] { -5.0d, 8.0d, -4.0d },
                new[] { 2.0d, 6.0d, 4.0d }
            });

            //act
            var cofactor1 = matrix.GetCofactor(0, 0);
            var cofactor2 = matrix.GetCofactor(0, 1);
            var cofactor3 = matrix.GetCofactor(0, 2);

            //assert
            Assert.AreEqual(56.0d, cofactor1);
            Assert.AreEqual(12.0d, cofactor2);
            Assert.AreEqual(-46.0d, cofactor3);
        }

        [TestMethod]
        public void GetCofactor_With4x4Matrix_ExpectCorrectValues()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -2.0d, -8.0d, 3.0d, 5.0d },
                new[] { -3.0d, 1.0d, 7.0d, 3.0d },
                new[] { 1.0d, 2.0d, -9.0d, 6.0d },
                new[] { -6.0d, 7.0d, 7.0d, -9.0d }
            });

            //act
            var cofactor1 = matrix.GetCofactor(0, 0);
            var cofactor2 = matrix.GetCofactor(0, 1);
            var cofactor3 = matrix.GetCofactor(0, 2);
            var cofactor4 = matrix.GetCofactor(0, 3);

            //assert
            Assert.AreEqual(690.0d, cofactor1);
            Assert.AreEqual(447.0d, cofactor2);
            Assert.AreEqual(210.0d, cofactor3);
            Assert.AreEqual(51.0d, cofactor4);
        }

        [TestMethod]
        public void IsInvertible_WithInvertible4x4Matrix_ExpectTrue()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 6.0d, 4.0d, 4.0d, 4.0d },
                new[] { 5.0d, 5.0d, 7.0d, 6.0d },
                new[] { 4.0d, -9.0d, 3.0d, -7.0d },
                new[] { 9.0d, 1.0d, 7.0d, -6.0d }
            });

            //act
            var isInvertible = matrix.IsInvertible;

            //assert
            Assert.IsTrue(isInvertible);
        }

        [TestMethod]
        public void IsInvertible_WithSingular4x4Matrix_ExpectFalse()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -4.0d, 2.0d, -2.0d, -3.0d },
                new[] { 9.0d, 6.0d, 2.0d, 6.0d },
                new[] { 0.0d, -5.0d, 1.0d, -5.0d },
                new[] { 0.0d, 0.0d, 0.0d, 0.0d }
            });

            //act
            var isInvertible = matrix.IsInvertible;

            //assert
            Assert.IsFalse(isInvertible);
        }

        [TestMethod]
        public void Inverse_WithInvertible4x4Matrix_ExpectCorrect4x4Matrix_1()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { -5.0d, 2.0d, 6.0d, -8.0d },
                new[] { 1.0d, -5.0d, 1.0d, 8.0d },
                new[] { 7.0d, 7.0d, -6.0d, -7.0d },
                new[] { 1.0d, -3.0d, 7.0d, 4.0d }
            });

            //act
            var inverse = matrix.Inverse;

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] {  0.21804511d,  0.45112782d,  0.24060150d, -0.04511278d },
                new[] { -0.80827067d, -1.45676692d, -0.44360902d,  0.52067669d },
                new[] { -0.07894737d, -0.22368421d, -0.05263158d,  0.19736842d },
                new[] { -0.52255639d, -0.81390977d, -0.30075188d,  0.30639098d }
            }), inverse);
        }

        [TestMethod]
        public void Inverse_WithInvertible4x4Matrix_ExpectCorrect4x4Matrix_2()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 8.0d, -5.0d, 9.0d, 2.0d },
                new[] { 7.0d, 5.0d, 6.0d, 1.0d },
                new[] { -6.0d, 0.0d, 9.0d, 6.0d },
                new[] { -3.0d, 0.0d, -9.0d, -4.0d }
            });

            //act
            var inverse = matrix.Inverse;

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -0.15384615d, -0.15384615d, -0.28205128d, -0.53846154d },
                new[] { -0.07692308d,  0.12307692d,  0.02564103d,  0.03076923d },
                new[] {  0.35897436d,  0.35897436d,  0.43589744d,  0.92307692d },
                new[] { -0.69230769d, -0.69230769d, -0.76923077d, -1.92307692d }
            }), inverse);
        }

        [TestMethod]
        public void Inverse_WithInvertible4x4Matrix_ExpectCorrect4x4Matrix_3()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 9.0d, 3.0d, 0.0d, 9.0d },
                new[] { -5.0d, -2.0d, -6.0d, -3.0d },
                new[] { -4.0d, 9.0d, 6.0d, 4.0d },
                new[] { -7.0d, 6.0d, 6.0d, 2.0d }
            });

            //act
            var inverse = matrix.Inverse;

            //assert
            Assert.AreEqual(new Matrix(new[]
            {
                new[] { -0.04074074d, -0.07777778d,  0.14444444d, -0.22222222d },
                new[] { -0.07777778d,  0.03333333d,  0.36666667d, -0.33333333d },
                new[] { -0.02901234d, -0.14629630d, -0.10925926d,  0.12962963d },
                new[] {  0.17777778d,  0.06666667d, -0.26666667d,  0.33333333d }
            }), inverse);
        }

        [TestMethod]
        public void Inverse_WithInvertible4x4Matrix_ExpectMultipliesWithOriginalMatrixToIdentityMatrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 3.0d, -9.0d, 7.0d, 3.0d },
                new[] { 3.0d, -8.0d, 2.0d, -9.0d },
                new[] { -4.0d, 4.0d, 4.0d, 1.0d },
                new[] { -6.0d, 5.0d, -1.0d, 1.0d }
            });
            var inverse = matrix.Inverse;

            //act
            var product1 = matrix * inverse;
            var product2 = inverse * matrix;

            //assert
            Assert.AreEqual(Matrix.Identity(4), product1);
            Assert.AreEqual(Matrix.Identity(4), product2);
        }

        [TestMethod]
        public void Inverse_WithInvertible4x4Matrix_ExpectInvertsToOriginalMatrix()
        {
            //arrange
            var matrix = new Matrix(new[]
            {
                new[] { 3.0d, -9.0d, 7.0d, 3.0d },
                new[] { 3.0d, -8.0d, 2.0d, -9.0d },
                new[] { -4.0d, 4.0d, 4.0d, 1.0d },
                new[] { -6.0d, 5.0d, -1.0d, 1.0d }
            });
            var inverse = matrix.Inverse;

            //act
            var secondInverse = inverse.Inverse;

            //assert
            Assert.AreEqual(matrix, secondInverse);
        }

        [TestMethod]
        public void Inverse_WithIdentityMatrix_ExpectNoChange()
        {
            //arrange
            var matrix = Matrix.Identity(4);

            //act
            var inverse = matrix.Inverse;

            //assert
            Assert.AreEqual(matrix, inverse);
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

#if CSHARP
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
#endif

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

#if CSHARP
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
#endif

        [TestMethod]
        public void Translation_WithVectorValues_ExpectMultipliesToCorrectPoint()
        {
            //arrange
            var vector = new Vector(new[] { 5.0d, -3.0d, 2.0d });
            var translation = Matrix.Translation(vector);
            var point = new Point3D(-3.0d, 4.0d, 5.0d);

            //act
            var newPoint = translation * point;

            //assert
            Assert.AreEqual(new Point3D(2.0d, 1.0d, 7.0d), newPoint);
        }

        [TestMethod]
        public void Translation_WithInverseOfTranslationMatrix_ExpectMultipliesToCorrectPoint()
        {
            //arrange
            var vector = new Vector(new[] { 5.0d, -3.0d, 2.0d });
            var translation = Matrix.Translation(vector).Inverse;
            var point = new Point3D(-3.0d, 4.0d, 5.0d);

            //act
            var newPoint = translation * point;

            //assert
            Assert.AreEqual(new Point3D(-8.0d, 7.0d, 3.0d), newPoint);
        }

        [TestMethod]
        public void Translation_WithVectorValues_ExpectMultipliesWithVectorToNoChange()
        {
            //arrange
            var translationVector = new Vector(new[] { 5.0d, -3.0d, 2.0d });
            var translation = Matrix.Translation(translationVector);
            var vector = new Vector3D(-3.0d, 4.0d, 5.0d);

            //act
            var newPoint = translation * vector;

            //assert
            Assert.AreEqual(vector, newPoint);
        }

        [TestMethod]
        public void Scale_MultipliedByPoint_ExpectCorrectNewPoint()
        {
            //arrange
            var scale = Matrix.Scale(new Vector(new[] { 2.0d, 3.0d, 4.0d }));
            var point = new Point3D(-4.0d, 6.0d, 8.0d);

            //act
            var newPoint = scale * point;

            //assert
            Assert.AreEqual(new Point3D(-8.0d, 18.0d, 32.0d), newPoint);
        }

        [TestMethod]
        public void Scale_MultipliedByVector_ExpectCorrectNewPoint()
        {
            //arrange
            var scale = Matrix.Scale(new Vector(new[] { 2.0d, 3.0d, 4.0d }));
            var point = new Vector3D(-4.0d, 6.0d, 8.0d);

            //act
            var newPoint = scale * point;

            //assert
            Assert.AreEqual(new Vector3D(-8.0d, 18.0d, 32.0d), newPoint);
        }

        [TestMethod]
        public void Scale_InvertedAndMultipliedByPoint_ExpectCorrectNewPoint()
        {
            //arrange
            var scale = Matrix.Scale(new Vector(new[] { 2.0d, 3.0d, 4.0d })).Inverse;
            var point = new Point3D(-4.0d, 6.0d, 8.0d);

            //act
            var newPoint = scale * point;

            //assert
            Assert.AreEqual(new Point3D(-2.0d, 2.0d, 2.0d), newPoint);
        }


        [TestMethod]
        public void Scale_WithScalarsOfOne_ExpectNoChangeToThePoint()
        {
            //arrange
            var scale = Matrix.Scale(new Vector(new[] { 1.0d, 1.0d, 1.0d }));
            var point = new Point3D(-4.0d, 6.0d, 8.0d);

            //act
            var newPoint = scale * point;

            //assert
            Assert.AreEqual(point, newPoint);
        }

        [TestMethod]
        public void Rotation_WithHalfQuarterRotationAboutXAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 1, 0);
            var halfQuarterRotation = Matrix.Rotation(Dimension.X, PI / 4);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(0, Sqrt(2) / 2, Sqrt(2) / 2), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithFullQuarterRotationAboutXAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 1, 0);
            var halfQuarterRotation = Matrix.Rotation(Dimension.X, PI / 2);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(0, 0, 1), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithHalfQuarterRotationAboutXAxis_ExpectCorrectInverseRotation()
        {
            //arrange
            var point = new Point3D(0, 1, 0);
            var halfQuarterRotation = Matrix.Rotation(Dimension.X, PI / 4);
            var inverseRotation = halfQuarterRotation.Inverse;

            //act
            var rotatedPoint = inverseRotation * point;

            //assert
            Assert.AreEqual(new Point3D(0, Sqrt(2) / 2, -Sqrt(2) / 2), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithHalfQuarterRotationAboutYAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 0, 1);
            var halfQuarterRotation = Matrix.Rotation(Dimension.Y, PI / 4);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(Sqrt(2) / 2, 0, Sqrt(2) / 2), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithFullQuarterRotationAboutYAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 0, 1);
            var halfQuarterRotation = Matrix.Rotation(Dimension.Y, PI / 2);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(1, 0, 0), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithHalfQuarterRotationAboutZAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 1, 0);
            var halfQuarterRotation = Matrix.Rotation(Dimension.Z, PI / 4);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(-Sqrt(2) / 2, Sqrt(2) / 2, 0), rotatedPoint);
        }

        [TestMethod]
        public void Rotation_WithFullQuarterRotationAboutZAxis_ExpectCorrectRotatedPoint()
        {
            //arrange
            var point = new Point3D(0, 1, 0);
            var halfQuarterRotation = Matrix.Rotation(Dimension.Z, PI / 2);

            //act
            var rotatedPoint = halfQuarterRotation * point;

            //assert
            Assert.AreEqual(new Point3D(-1, 0, 0), rotatedPoint);
        }

        [TestMethod]
        public void Shear_WithXInProportionToY_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.X, Dimension.Y, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(5, 3, 4), shearedPoint);
        }

        [TestMethod]
        public void Shear_WithXInProportionToZ_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.X, Dimension.Z, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(6, 3, 4), shearedPoint);
        }


        [TestMethod]
        public void Shear_WithYInProportionToX_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.Y, Dimension.X, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(2, 5, 4), shearedPoint);
        }


        [TestMethod]
        public void Shear_WithYInProportionToZ_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.Y, Dimension.Z, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(2, 7, 4), shearedPoint);
        }


        [TestMethod]
        public void Shear_WithZInProportionToX_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.Z, Dimension.X, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(2, 3, 6), shearedPoint);
        }

        [TestMethod]
        public void Shear_WithZInProportionToY_ExpectCorrectShearedPoint()
        {
            //arrange
            var point = new Point3D(2, 3, 4);
            var shear = Matrix.Shear(Dimension.Z, Dimension.Y, 1);

            //act
            var shearedPoint = shear * point;

            //assert
            Assert.AreEqual(new Point3D(2, 3, 7), shearedPoint);
        }
    }
}
