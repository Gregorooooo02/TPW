using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Data.Tests
{
    [TestClass]
    public class DataTest
    {
        private AbstractBallDataAPI ball;

        [TestInitialize]
        public void Setup()
        {
            Vector2 position = new Vector2(2, 2);
            int deltaX = 1;
            int deltaY = 1;
            int size = 10;
            int mass = 5;
            bool isSimulationRunning = false;

            ball = AbstractBallDataAPI.CreateInstance(position, deltaX, deltaY, size, mass, isSimulationRunning);
        }



        [TestMethod]
        public void BallAPI_CreateBallAPITest()
        {

            Assert.IsNotNull(ball);
            Assert.IsInstanceOfType(ball, typeof(AbstractBallDataAPI));
        }

        [TestMethod]
        public void BallAPI_PositionTest()
        {
            Vector2 position = new Vector2(2, 2);

            var excpectedPosition = ball.Position;

            Assert.AreEqual(excpectedPosition, position);
        }


        [TestMethod]
        public void BallAPI_GetX()
        {


            int x = ball.PositionX;


            Assert.AreEqual(2, x);
        }

        [TestMethod]
        public void BallAPI_GetY()
        {

            int y = ball.PositionY;


            Assert.AreEqual(2, y);
        }

        [TestMethod]
        public void BallAPI_GetDiameter()
        {

            int diameter = ball.Diameter;


            Assert.AreEqual(20, diameter);
        }

        [TestMethod]
        public void BallAPI_GetMass()
        {

            int mass = ball.Mass;

            Assert.AreEqual(5, mass);
        }

        [TestMethod]
        public void BallAPI_GetSize()
        {

            int size = ball.Radius;


            Assert.AreEqual(10, size);
        }

        [TestMethod]
        public void BallAPI_SetVelocity()
        {

            int newDeltaX = 10;
            int newDeltaY = 10;

            ball.setVelocity(newDeltaX, newDeltaY);


            Assert.AreEqual(10, ball.VelocityX);
            Assert.AreEqual(10, ball.VelocityY);
        }

        [TestMethod]
        public void BallAPI_IsSimulationRunning_SetValue()
        {

            bool newValue = true;


            ball.isRunning = newValue;


            Assert.IsTrue(ball.isRunning);
        }

        [TestMethod]
        public void BallAPI_IsSimulationRunning_GetValue()
        {

            Assert.IsFalse(ball.isRunning);
        }

        [TestMethod]
        public void BallAPI_VxGetter()
        {
            int expectedVx = 1;

            int actualVx = ball.VelocityX;

            Assert.AreEqual(expectedVx, actualVx);
        }

        [TestMethod]
        public void BallAPI_VxSetter()
        {
            int Vx = 5;

            ball.VelocityX = Vx;

            // Assert
            Assert.AreEqual(ball.VelocityX, Vx);
        }

        [TestMethod]
        public void BallAPI_VyGetter()
        {
            int expectedVy = 1;

            int actualVy = ball.VelocityY;

            Assert.AreEqual(expectedVy, actualVy);
        }

        [TestMethod]
        public void BallAPI_VySetter()
        {
            int Vy = 5;

            ball.VelocityY = Vy;

            // Assert
            Assert.AreEqual(ball.VelocityY, Vy);
        }
    }


    [TestClass]
    public class DataAPITest
    {
        private AbstractDataAPI data;

        [TestInitialize]
        public void Setup()
        {
            int boardWidth = 500;
            int boardHeight = 400;
            data = AbstractDataAPI.CreateInstance(boardWidth, boardHeight);
        }

        [TestMethod]
        public void DataAPI_getBoardWidth()
        {
            int expectedValue = data.getWindowWidth();

            Assert.AreEqual(expectedValue, 500);
        }
        [TestMethod]
        public void DataAPI_getBoardHeight()
        {
            int expectedValue = data.getWindowHeight();

            Assert.AreEqual(expectedValue, 400);
        }
    }
}
