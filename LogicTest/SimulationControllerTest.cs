using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SimulationControllerTest
    {
        private AbstractLogicAPI controller;

        public SimulationControllerTest()
        {
            controller = AbstractLogicAPI.CreateInstance();
        }

        [TestMethod]
        public void BallSpawningTest()
        {
            controller.SpawnBalls(2);
            Assert.AreEqual(controller.Balls.Count(), 2);
        }

        [TestMethod]
        public void SimulationTest()
        {
            controller.SpawnBalls(2);

            float positionX = controller.Balls.First().Position.X;

            controller.StartSim();
            Thread.Sleep(100);
            controller.StopSim();

            Assert.AreNotEqual(controller.Balls.First().Position.X, positionX);
        }
    }
}
