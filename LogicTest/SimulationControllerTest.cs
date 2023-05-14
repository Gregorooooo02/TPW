﻿using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class SimulationControllerTests
    {
        private readonly int _testWidth;
        private readonly int _testHeight;
        private readonly int _testMinDiameter;
        private readonly int _testMaxDiameter;
        private readonly AbstractDataAPI _dataFixture;

        private AbstractLogicAPI _controller;
        private IEnumerable<Logic.IBall>? _balls;

        public SimulationControllerTests()
        {
            _controller = AbstractLogicAPI.CreateInstance(_dataFixture);
            _dataFixture = new DataFixture();

            _testWidth = _dataFixture.WindowWidth;
            _testHeight = _dataFixture.WindowHeight;
            _testMinDiameter = _dataFixture.MinDiameter;
            _testMaxDiameter = _dataFixture.MaxDiameter;
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_controller);
        }

        [TestMethod]
        public void SimulationTest()
        {
            _controller = AbstractLogicAPI.CreateInstance(_dataFixture);
            Assert.IsNotNull(_controller);

            _balls = _controller.CreateBalls(2);
            Assert.AreEqual(_balls.Count(), 2);

            float xPos = _balls.First().Position.X;
            Thread.Sleep(100);
            _balls.First().Move(1f);
            Assert.AreNotEqual(_balls.First().Position.X, xPos);
        }

        [TestMethod]
        public void RandomBallsCreationTest()
        {
            _controller = AbstractLogicAPI.CreateInstance(_dataFixture);
            Assert.IsNotNull(_controller);

            int ballNumber = 10;

            var balls = _controller.CreateBalls(ballNumber);
            int counter = 0;

            foreach (Ball b in balls)
            {
                Assert.IsNotNull(b);
                Assert.IsTrue(IsBetween(b.Diameter, _testMinDiameter, _testMaxDiameter));
                Assert.IsTrue(IsBetween(b.Radius, _testMinDiameter / 2, _testMaxDiameter / 2));
                Assert.IsTrue(IsBetween(b.Position.X, 0, _testWidth));
                Assert.IsTrue(IsBetween(b.Position.Y, 0, _testHeight));
                counter++;
            }
            Assert.AreEqual(ballNumber, counter);
        }

        private static bool IsBetween(float value, float bottom, float top)
        {
            return value <= top && value >= bottom;
        }

        private class DataFixture : AbstractDataAPI
        {
            public override int WindowHeight => 100;
            public override int WindowWidth => 100;
            public override float MaxSpeed => 50;
            public override int MinDiameter => 20;
            public override int MaxDiameter => 50;
        }
    }

}