using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;
using RayVE.Materials;
using RayVE.Surfaces;
using System.Linq;

namespace RayVE.Tests.Materials
{
    [TestClass]
    public class RingPatternTests
    {
        [TestMethod]
        public void ColorAt_With2ColorRing_ExpectCorrectColors()
        {
            //arrange
            var color1 = Color.White;
            var color2 = Color.Black;
            var pattern = new RingPattern(color1, color2);
            var material = new PhongMaterial(PhongMaterial.Default, pattern);
            var surface = new Sphere(material);
            var points = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(1, 0, 0),
                new Point3D(0, 0, 1),
                new Point3D(0.708, 0, 0.708)
            };

            //act
            var colors = points
                .Select(p => pattern.ColorAt(p, surface))
                .ToArray();

            //assert
            var expected = new[]
            {
                Color.White,
                Color.Black,
                Color.Black,
                Color.Black
            };
            CollectionAssert.AreEqual(expected, colors);
        }
    }
}
