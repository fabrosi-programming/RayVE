using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE;
using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayVE.Tests
{
    [TestClass]
    public class ViewTransformationTests
    {
        [TestMethod]
        public void Constructor_WithDefaultInputs_ExpectMatrixIsIdentityMatrix()
        {
            //arrange
            var position = new Point3D(0, 0, 0);
            var target = new Point3D(0, 0, -1);
            var up = new Vector3D(0, 1, 0);

            //act
            var camera = new ViewTransformation(position, target, up);

            //assert
            Assert.AreEqual(Matrix.Identity(4), camera.Matrix);
        }

        [TestMethod]
        public void Constructor_WithInvertedTargetDirection_ExpectMatrixIsScaleMatrix()
        {
            //arrange
            var position = new Point3D(0, 0, 0);
            var target = new Point3D(0, 0, 1);
            var up = new Vector3D(0, 1, 0);

            //act
            var camera = new ViewTransformation(position, target, up);

            //assert
            Assert.AreEqual(Matrix.Scale(new Vector(-1, 1, -1)), camera.Matrix);
        }

        [TestMethod]
        public void Constructor_WithNonOriginPosition_ExpectMatrixIsTranslationMatrix()
        {
            //arrange
            var position = new Point3D(0, 0, 8);
            var target = new Point3D(0, 0, 0);
            var up = new Vector3D(0, 1, 0);

            //act
            var camera = new ViewTransformation(position, target, up);

            //assert
            Assert.AreEqual(Matrix.Translation(new Vector(0, 0, -8)), camera.Matrix);
        }

        [TestMethod]
        public void Constructor_WithArbitraryInputs_ExpectCorrectMatrix()
        {
            //arrange
            var position = new Point3D(1, 3, 2);
            var target = new Point3D(4, -2, 8);
            var up = new Vector3D(1, 1, 0);

            //act
            var camera = new ViewTransformation(position, target, up);

            //assert
            var expected = new Matrix(new[]
            {
                new[] { -0.50709255283711, 0.50709255283711, 0.67612340378281321, -2.3664319132398464 },
                new[] { 0.76771593385968018, 0.60609152673132649, 0.12121830534626529, -2.8284271247461903 },
                new[] { -0.35856858280031811, 0.59761430466719678, -0.71713716560063623, 0.0},
                new[] { 0.0, 0.0, 0.0, 1.0 }
            });
            Assert.AreEqual(expected, camera.Matrix);
        }
    }
}
