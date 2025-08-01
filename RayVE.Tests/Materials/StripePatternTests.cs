using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayVE.Tests.Materials
{
    [TestClass]
    public class StripePatternTests
    {
        [TestMethod]
        public void ColorAt_WithPointsVaryingInY_ExpectSameColor()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new StripePattern(color1, color2);
            var points = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0, 1, 0),
                new Point3D(0, 2, 0)
            };
            var surface = new Sphere();

            //act
            var testColors = points
                .Select(p => pattern.ColorAt(p, surface))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                Color.White,
                Color.White
            };
            CollectionAssert.AreEqual(expected, testColors);
        }

        [TestMethod]
        public void ColorAt_WithPointsVaryingInZ_ExpectSameColor()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new StripePattern(color1, color2);
            var points = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0, 0, 1),
                new Point3D(0, 0, 2)
            };
            var surface = new Sphere();

            //act
            var testColors = points
                .Select(p => pattern.ColorAt(p, surface))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                Color.White,
                Color.White
            };
            CollectionAssert.AreEqual(expected, testColors);
        }

        [TestMethod]
        public void ColorAt_WithPointsVaryingInX_ExpectDifferentColors()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new StripePattern(color1, color2);
            var points = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0.9, 0, 0),
                new Point3D(1, 0, 0),
                new Point3D(-0.1, 0, 0),
                new Point3D(-1, 0, 0),
                new Point3D(-1.1, 0, 0),
            };
            var surface = new Sphere();

            //act
            var testColors = points
                .Select(p => pattern.ColorAt(p, surface))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                Color.White,
                Color.Black,
                Color.Black,
                Color.Black,
                Color.White
            };
            CollectionAssert.AreEqual(expected, testColors);
        }

        [TestMethod]
        public void ColorAt_WithSurfaceTransformation_ExpectCorrectColors()
        {
            //arrange
            var pattern = new StripePattern(
                Color.White,
                Color.Black);
            var surface = new Sphere(
                Matrix.Scale(new Vector(2, 2, 2)),
                new PhongMaterial(
                    PhongMaterial.Default,
                    pattern));
            var point = new Point3D(1.5, 0, 0);

            //act
            var color = pattern.ColorAt(point, surface);

            //assert
            Assert.AreEqual(Color.White, color);
        }

        [TestMethod]
        public void ColorAt_WithPatternTransformation_ExpectCorrectColors()
        {
            //arrange
            var pattern = new StripePattern(
                Color.White,
                Color.Black,
                Matrix.Scale(new Vector(2, 2, 2)));
            var surface = new Sphere(
                Matrix.Scale(new Vector(2, 2, 2)),
                new PhongMaterial(
                    PhongMaterial.Default,
                    pattern));
            var point = new Point3D(1.5, 0, 0);

            //act
            var color = pattern.ColorAt(point, surface);

            //assert
            Assert.AreEqual(Color.White, color);
        }

        [TestMethod]
        public void ColorAt_WithBothSurfaceTransformationAndPatternTransformation_ExpectCorrectColors()
        {
            //arrange
            var pattern = new StripePattern(
                Color.White,
                Color.Black,
                Matrix.Translation(new Vector(0.5, 0, 0)));
            var surface = new Sphere(
                Matrix.Scale(new Vector(2, 2, 2)),
                new PhongMaterial(
                    PhongMaterial.Default,
                    pattern));
            var point = new Point3D(2.5, 0, 0);

            //act
            var color = pattern.ColorAt(point, surface);

            //assert
            Assert.AreEqual(Color.White, color);
        }
    }
}
