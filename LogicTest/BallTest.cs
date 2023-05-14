using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BallTest
    {
        private static readonly int TestXPos = 5;
        private static readonly int TestYPos = 5;
        private static readonly float TestXSpeed = 0.2f;
        private static readonly float TestYSpeed = -0.1f;
        private static readonly int TestDiameter = 2;
        float acceptableDifference = 0.1f;

        private readonly Ball _testBall;
        private readonly Window _testBoard;

        public BallTest()
        {
            Vector2 position = new Vector2(TestXPos, TestYPos);
            Vector2 speed = new Vector2(TestXSpeed, TestYSpeed);

            _testBoard = new Window(100, 100);
            _testBall = new Ball(TestDiameter, position, speed, _testBoard);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_testBall);

            Assert.AreEqual((int)_testBall.Position.X, TestXPos);
            Assert.AreEqual((int)_testBall.Position.Y, TestYPos);
            Assert.AreEqual(_testBall.Diameter, TestDiameter);
        }

        [TestMethod]
        public void MoveTest()
        {
            float delta = 100f;
            Ball ball = new Ball(TestDiameter, new Vector2(TestXPos, TestYPos), Vector2.Zero, _testBoard);

            ball.Velocity = new Vector2(0f, -2.5f);
            Assert.AreEqual(0f, ball.Velocity.X);

            ball.Move(delta);
            Assert.AreEqual(TestXPos, ball.Position.X);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            ball.Velocity = new Vector2(3f, 5f);
            Assert.AreEqual(ball.Velocity, new Vector2(3f, 5f));

            ball.Move(delta);
            Assert.AreEqual(5f, ball.Position.Y, acceptableDifference);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            Assert.AreEqual(17f, ball.Position.X, acceptableDifference);
            Assert.AreEqual(20f, ball.Position.Y, acceptableDifference);
        }

        [TestMethod]
        public void EqualTest()
        {
            Ball newBall = _testBall;
            Assert.AreEqual(_testBall, newBall);
        }
    }
}