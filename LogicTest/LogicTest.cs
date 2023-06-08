using Logic;
using Data;
using System.Numerics;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Logic.Tests
{
    [TestClass]
    public class BallsAPITests
    {
        private AbstractBallAPI ballsAPI;

        public class TestData : AbstractDataAPI
        {
            private int _windowWidth;
            private int _windowHeight;
            private bool isLoggerRunning;
            private readonly object fileLock = new object();
            private readonly Stopwatch _stopwatch = new Stopwatch();

            public TestData(int windowWidth,int windowHeight) 
            {
                _windowHeight = windowHeight;
                _windowWidth = windowWidth;
            }

            public override AbstractBallDataAPI spawnBall(bool isSimulationRunning)
            {
                Random random = new Random();
                int x = random.Next(20, _windowWidth - 20);
                int y = random.Next(20, _windowHeight - 20);
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
                return _windowHeight;
            }

            public override int getWindowWidth()
            {
                return _windowWidth;
            }

            public override void LoggerStop()
            {
                isLoggerRunning = false;
            }

            public override async Task LoggerStart(ConcurrentQueue<AbstractBallDataAPI> queue)
            {
                isLoggerRunning = true;
                await Logger(queue);
            }

            private async Task Logger(ConcurrentQueue<AbstractBallDataAPI> queue)
            {
                while (isLoggerRunning)
                {
                    _stopwatch.Restart();
                    queue.TryDequeue(out AbstractBallDataAPI ball);
                    if (ball != null)
                    {
                        string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), JsonSerializer.Serialize(ball)) + "}";

                        lock (fileLock)
                        {
                            using (var stream = new StreamWriter("..\\..\\..\\..\\..\\Logger.json", true, Encoding.UTF8))
                            {
                                stream.WriteLine(log);
                            }
                        }
                    }
                    _stopwatch.Stop();
                    await Task.Delay((int)_stopwatch.ElapsedMilliseconds + 100);
                }
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
            Assert.AreEqual(1, ballsAPI.GetNumberOfBalls());
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
