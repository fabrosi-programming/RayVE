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
    public class GradientPatternTests
    {
        [TestMethod]
        public void ColorAt_With2ColorGradient_ExpectCorrectColors()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new GradientPattern(color1, color2);
            var material = new PhongMaterial(PhongMaterial.Default, pattern: pattern);
            var surface = new Sphere(material);
            var points = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0.25, 0, 0),
                new Point3D(0.5, 0, 0),
                new Point3D(0.75, 0, 0)
            };

            //act
            var colors = points
                .Select(p => pattern.ColorAt(p, surface))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                new Color(0.75, 0.75, 0.75),
                new Color(0.5, 0.5, 0.5),
                new Color(0.25, 0.25, 0.25)
            };
            CollectionAssert.AreEqual(expected, colors);
        }
    }
}
