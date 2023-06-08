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
    public abstract class AbstractDataAPI
    {
        public abstract AbstractBallDataAPI spawnBall(bool isSimulationRunning);
        public abstract int getWindowWidth();
        public abstract int getWindowHeight();
        public abstract void LoggerStop();
        public abstract Task LoggerStart(ConcurrentQueue<AbstractBallDataAPI> queue);
        public static ConcurrentQueue<AbstractBallDataAPI> ballQueue = AbstractBallDataAPI.BallQueue;
        public static AbstractDataAPI CreateInstance(int windowWidth, int windowHeight)
        {
            return new Data(windowWidth, windowHeight);
        }
    }
}