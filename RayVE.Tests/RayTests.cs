using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.LinearAlgebra;

namespace RayVE.Tests
{
    [TestClass]
    public class RayTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectPropertyValues()
        {
            //arrange
            var origin = new Vector(0, 1, 2);
            var direction = new Vector(3, 4, 5);

            //act
            var ray = new Ray(origin, direction);

            //assert
            Assert.AreEqual(origin, ray.Origin);
            Assert.AreEqual(direction, ray.Direction);
        }

        [TestMethod]
        public void Position_WithValidInputs_ExpectCorrectVector()
        {
            //arrange
            var origin = new Vector(2, 3, 4);
            var direction = new Vector(1, 0, 0);
            var ray = new Ray(origin, direction);

            //act
            var position1 = ray.PositionAtDistance(0);
            var position2 = ray.PositionAtDistance(1);
            var position3 = ray.PositionAtDistance(-1);
            var position4 = ray.PositionAtDistance(2.5);

            //assert
            Assert.AreEqual(new Vector(2, 3, 4), position1);
            Assert.AreEqual(new Vector(3, 3, 4), position2);
            Assert.AreEqual(new Vector(1, 3, 4), position3);
            Assert.AreEqual(new Vector(4.5, 3, 4), position4);
        }
    }
}
