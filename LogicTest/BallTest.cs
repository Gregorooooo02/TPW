using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BallTest
    {
        private static readonly float testVelocityX = 0.2f;
        private static readonly float testVelocityY = -0.1f;
        private static readonly int testPositionX = 5;
        private static readonly int testPositionY = 5;
        private static readonly int testDiameter = 2;

        Ball testBall;

        public BallTest()
        {
            Vector2 position = new Vector2(testPositionX, testPositionY);
            Vector2 velocity = new Vector2(testVelocityX, testVelocityY);

            testBall = new Ball(velocity, position, testDiameter);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(testBall);

            Assert.AreEqual((int)testBall.Position.X, testPositionX);
            Assert.AreEqual((int)testBall.Position.Y, testPositionY);
            Assert.AreEqual(testBall.Diameter, testDiameter);
        }

        [TestMethod]
        public void MoveTest()
        {
            Ball ball = new Ball(new Vector2(0, 0), new Vector2(testPositionX, testPositionY), testDiameter);
            Vector2 xBorder = new Vector2(0, 100);
            Vector2 yBorder = new Vector2(0, 100);

            ball.IncreaseVelocity(new Vector2(-2.5f, 0));
            Assert.AreEqual(ball.Velocity.X, -2.5f);

            ball.Move(xBorder, yBorder);
            Assert.AreEqual(ball.Position.Y, testPositionY);
            Assert.AreEqual(ball.Position.X, testPositionX - 2.5f);

            ball.Move(xBorder, yBorder);
            ball.Move(xBorder, yBorder);
            ball.Move(xBorder, yBorder);

            ball.IncreaseVelocity(new Vector2(-2.5f, -2.5f));
            Assert.AreEqual(ball.Velocity, new Vector2(0, -2.5f));

            ball.Move(xBorder, yBorder);
            Assert.AreEqual(ball.Position.Y, testPositionY - 2.5f);
        }

        [TestMethod]
        public void EqualTest()
        {
            Ball secondBall = testBall;
            Assert.AreEqual(testBall, secondBall);
        }
    }
}