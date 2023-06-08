using Logic;
using Data;
using System.Numerics;
using System.Collections.Concurrent;

namespace UnitTestProject
{
    [TestClass]
    public class BallsAPITests
    {
        private AbstractBallAPI ballsAPI;

        public class TestData : AbstractDataAPI
        {
            private int _boardWidth;
            private int _boardHeight;
            public TestData(int boardWidth,int boardHeight) 
            {
                _boardHeight = boardHeight;
                _boardWidth = boardWidth;
            }

            public override AbstractBallDataAPI spawnBall(bool isSimulationRunning)
            {
                Random random = new Random();
                int x = random.Next(20, _boardWidth - 20);
                int y = random.Next(20, _boardHeight - 20);
                int valueX = random.Next(-3, 4);
                int valueY = random.Next(-3, 4);
                Vector2 position = new Vector2((int)x, (int)y);

                if (valueX == 0)
                {
                    valueX = random.Next(1, 3) * 2 - 3;
                }
                if (valueY == 0)
                {
                    valueY = random.Next(1, 3) * 2 - 3;
                }

                int Vx = valueX;
                int Vy = valueY;
                int radius = 20;
                int mass = 200;
                return AbstractBallDataAPI.CreateInstance(position, Vx, Vy, radius, mass, isSimulationRunning);
            }
            public override int getWindowHeight()
            {
                return _boardHeight;
            }

            public override int getWindowWidth()
            {
                return _boardWidth;
            }

            public override void LoggerStop()
            {
                throw new NotImplementedException();
            }

            public override Task LoggerStart(ConcurrentQueue<AbstractBallDataAPI> queue)
            {
                throw new NotImplementedException();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ballsAPI = AbstractBallAPI.CreateInstance(300,150,new TestData(300,150));
        }

        [TestMethod]
        public void TestCreateBall()
        {
            ballsAPI.SpawnBall();
            ballsAPI.StopSimulation();
            Assert.AreEqual(1, ballsAPI.GetNumberOfBalls());

          
            int x = ballsAPI.GetPositionX(0);
            int y = ballsAPI.GetPositionY(0);
            int size = ballsAPI.GetSize(0);
            Assert.IsTrue(x >= 0);
            Assert.IsTrue(x <= 300);
            Assert.IsTrue(y >= 0);
            Assert.IsTrue(x <= 150);
            Assert.AreEqual(20, size);

        }


        [TestMethod]
        public void CreateBall_AddsNewBallToList()
        {
            ballsAPI.SpawnBall();

            Assert.IsNotNull(ballsAPI.balls);
            //Assert.AreEqual(1, ballsAPI.GetNumberOfBalls());
        }

        [TestMethod]
        public void Test_GetAllBalls()
        {
            int expectedCount = 1;
            ballsAPI.SpawnBall();


            List<AbstractBallDataAPI> balls = ballsAPI.balls;


            Assert.AreEqual(expectedCount, balls.Count);
        }

    }
}
