using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE;

namespace RayVE.Tests
{
    [TestClass]
    public class ColorTests
    {
        [TestMethod]
        public void Constructor_WithValidInput_ExpectCorrectRGBValues()
        {
            //arrange-act
            var color = new Color(-0.5d, 0.4d, 1.7d);

            //assert
            Assert.AreEqual(-0.5d, color.R);
            Assert.AreEqual(0.4d, color.G);
            Assert.AreEqual(1.7d, color.B);
        }

        [TestMethod]
        public void AdditionOperator_WithValidColors_ExpectCorrectColor()
        {
            //arrange
            var color1 = new Color(0.9d, 0.6d, 0.75d);
            var color2 = new Color(0.7d, 0.1d, 0.25d);

            //act
            var sum1 = color1 + color2;
            var sum2 = color2 + color1;

            //assert
            Assert.AreEqual(new Color(1.6d, 0.7d, 1.0d), sum1);
            Assert.AreEqual(new Color(1.6d, 0.7d, 1.0d), sum2);
        }

        [TestMethod]
        public void MinusOperator_WithValidColors_ExpectCorrectColor()
        {
            //arrange
            var color1 = new Color(0.9d, 0.6d, 0.75d);
            var color2 = new Color(0.7d, 0.1d, 0.25d);

            //act
            var difference1 = color1 - color2;
            var difference2 = color2 - color1;

            //assert
            Assert.AreEqual(new Color(0.2d, 0.5d, 0.5d), difference1);
            Assert.AreEqual(new Color(-0.2d, -0.5d, -0.5d), difference2);
        }

        [TestMethod]
        public void MultiplyOperator_WithNonZeroScalar_ExpectCorrectColor()
        {
            //arrange
            var color = new Color(0.2d, 0.3d, 0.4d);
            var scalar = 2.0d;

            //act
            var scaled = color * scalar;

            //assert
            Assert.AreEqual(new Color(0.4d, 0.6d, 0.8d), scaled);
        }

        [TestMethod]
        public void MultiplyOperator_WithTwoColors_ExpectCorrectColor()
        {
            //arrange
            var color1 = new Color(1.0d, 0.2d, 0.4d);
            var color2 = new Color(0.9d, 1.0d, 0.1d);

            //act
            var multiplied1 = color1 * color2;
            var multiplied2 = color2 * color1;

            //assert
            Assert.AreEqual(new Color(0.9d, 0.2d, 0.04d), multiplied1);
            Assert.AreEqual(new Color(0.9d, 0.2d, 0.04d), multiplied2);
        }
    }
}
