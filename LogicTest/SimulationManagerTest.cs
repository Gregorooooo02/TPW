using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SimulationManagerTest
    {
        private const int testDiameter = 10;
        private const int testRadius = testDiameter / 2;
        private const int testWidth = 100;
        private const int testHeight = 100;
        private SimulationManager ballManager = new(new Window(testWidth, testHeight), testDiameter);

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(ballManager);
        }

        [TestMethod]
        public void RandomizedSpawningTest()
        {
            int numberOfBalls = 10;
            int counter = 0;
            IEnumerable<Ball> balls = ballManager.RandomizedBallSpawning(numberOfBalls);

            Assert.IsNotNull(ballManager);

            foreach (Ball ball in balls)
            {
                Assert.IsNotNull(ball);
                Assert.AreEqual(testDiameter, ball.Diameter);
                Assert.IsTrue(BallInBoundries(ball.Position.X, testWidth, 0));
                Assert.IsTrue(BallInBoundries(ball.Position.Y, testHeight, 0));

                counter++;
            }
            Assert.AreEqual(numberOfBalls, counter);
        }

        private static bool BallInBoundries(float value, float top, float bottom)
        {
            return value <= top - testRadius && value >= bottom + testRadius;
        }
    }
}
