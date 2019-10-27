using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayVE.Surfaces;

namespace RayVE.Tests
{
    [TestClass]
    public class IntersectionTests
    {
        [TestMethod]
        public void Constructor_WithValidInputs_ExpectCorrectFieldValues()
        {
            //arrange
            var distance = 3.5;
            var surface = new Sphere(3);

            //act
            var intersection = new Intersection(distance, surface);

            //assert
            Assert.AreEqual(distance, intersection.Distance);
            Assert.AreEqual(surface, intersection.Surface);
        }
    }
}
