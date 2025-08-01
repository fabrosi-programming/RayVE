using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RayVE.Tests
{
    [TestClass]
    public class CanvasTests
    {
        [TestMethod]
        public void Constructor_WithValidDimensions_ExpectCorrectDimensions()
        {
            //arrange-act
            var canvas = new Canvas(10, 20);

            //assert
            Assert.AreEqual(10u, canvas.Width);
            Assert.AreEqual(20u, canvas.Height);
        }

        [TestMethod]
        public void Constructor_WithValidDimensions_ExpectAllPixelsAreBlack()
        {
            //arrange-act
            var canvas = new Canvas(10, 20);

            //assert
            for (var x = 0; x < 10; x++)
                for (var y = 0; y < 20; y++)
                    Assert.AreEqual(canvas[x, y], Color.Black);
        }

        [TestMethod]
        public void Set_WithValidInputs_ExpectCorrectPixelColor()
        {
            //arrange
            var canvas = new Canvas(10, 20);

            //act
            canvas[2, 3] = Color.Red;

            //assert
            Assert.AreEqual(Color.Red, canvas[2, 3]);
        }
    }
}