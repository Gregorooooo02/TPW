using Logic;
using Data;
using System.Numerics;


namespace UnitTestProject
{
    [TestClass]
    public class BallsAPITests
    {
        private AbstractBallAPI ballsApi;

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
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ballsApi = AbstractBallAPI.CreateInstance(300,150,new TestData(300,150));
        }

        [TestMethod]
        public void TestCreateBall()
        {
            ballsApi.SpawnBall();
            ballsApi.StopSimulation();
            Assert.AreEqual(1, ballsApi.GetNumberOfBalls());

          
            int x = ballsApi.GetPositionX(0);
            int y = ballsApi.GetPositionY(0);
            int size = ballsApi.GetSize(0);
            Assert.IsTrue(x >= 0);
            Assert.IsTrue(x <= 300);
            Assert.IsTrue(y >= 0);
            Assert.IsTrue(x <= 150);
            Assert.AreEqual(20, size);

        }


        [TestMethod]
        public void CreateBall_AddsNewBallToList()
        {


            ballsApi.SpawnBall();


            Assert.IsNotNull(ballsApi.balls);
            Assert.AreEqual(1, ballsApi.GetNumberOfBalls());
        }

        [TestMethod]
        public void Test_GetAllBalls()
        {
            int expectedCount = 1;
            ballsApi.SpawnBall();


            List<AbstractBallDataAPI> balls = ballsApi.balls;


            Assert.AreEqual(expectedCount, balls.Count);
        }

    }
}
