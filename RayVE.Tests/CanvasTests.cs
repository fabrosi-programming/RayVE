using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual(10, canvas.Width);
            Assert.AreEqual(20, canvas.Height);
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

        [TestMethod]
        public void ToPPM_WithValidCanvas_ExpectCorrectHeaderRows()
        {
            //arrange
            var canvas = new Canvas(5, 3);
            var maxValue = 255;

            //act
            var ppm = canvas.ToPPM(maxValue)
                            .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //assert
            Assert.AreEqual("P3", ppm[0]);
            Assert.AreEqual("5 3", ppm[1]);
            Assert.AreEqual("255", ppm[2]);
        }

        [TestMethod]
        public void ToPPM_WithValidCanvas_ExpectCorrectPixelData()
        {
            //arrange
            var canvas = new Canvas(5, 3);
            canvas[0, 0] = new Color(1.5d, 0.0d, 0.0d);
            canvas[2, 1] = new Color(0.0d, 0.5d, 0.0d);
            canvas[4, 2] = new Color(-0.5d, 0.0d, 1.0d);
            var maxValue = 255;

            //act
            var ppm = canvas.ToPPM(maxValue)
                            .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //assert
            Assert.AreEqual("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", ppm[3]);
            Assert.AreEqual("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", ppm[4]);
            Assert.AreEqual("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", ppm[5]);
        }

        [TestMethod]
        public void ToPPM_WithWideCanvas_ExpectNoMoreThan70CharactersPerLine()
        {
            //arrange
            var canvas = new Canvas(10, 20);
            canvas.Fill(new Color(1.0d, 0.8d, 0.6d));
            var maxValue = 255;

            //act
            var ppm = canvas.ToPPM(maxValue)
                            .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //assert
            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppm[3]);
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppm[4]);
            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppm[5]);
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppm[6]);
        }

        [TestMethod]
        public void ToPPM_WithValidCanvas_ExpectLastCharacterIsNewLine()
        {
            //arrange
            var canvas = new Canvas(5, 3);
            var maxValue = 255;

            //act
            var ppm = canvas.ToPPM(maxValue);

            //assert
            Assert.IsTrue(ppm.EndsWith(Environment.NewLine));
        }
    }
}
