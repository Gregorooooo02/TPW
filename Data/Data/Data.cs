using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class Data : AbstractDataAPI
    {
        private int _windowWidth;
        private int _windowHeight;
        private bool isLoggerRunning;
      
        private readonly object fileLock = new object();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Data(int windowWidth, int windowHeight)

        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public override int getWindowWidth()
        {
            return _windowWidth;
        }

        public override int getWindowHeight()
        {
            return _windowHeight;
        }

        public override AbstractBallDataAPI spawnBall(bool isSimulationRunning)
        {
            Random random = new Random();
            int x = random.Next(20, _windowWidth - 20);
            int y = random.Next(20, _windowHeight - 20);
            int varX = random.Next(-3, 4);
            int varY = random.Next(-3, 4);
            Vector2 position = new Vector2((int)x, (int)y);

            if (varX == 0)
            {
                varX = random.Next(1, 3) * 2 - 3;
            }
            if (varY == 0)
            {
                varY = random.Next(1, 3) * 2 - 3;
            }

            int Vx = varX;
            int Vy = varY;
            int radius = 20;
            int mass = 200;
            return AbstractBallDataAPI.CreateInstance(position, Vx, Vy, radius, mass, isSimulationRunning);
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
}
