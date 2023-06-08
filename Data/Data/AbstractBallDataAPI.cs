using System.Collections.Concurrent;
using System.ComponentModel;
using System.Numerics;

namespace Data
{
    public abstract class AbstractBallDataAPI
    {
        public abstract Vector2 Position { get; }

        public abstract int X { get; }
        public abstract int Y { get; }

        public abstract bool isSimulationRunning { get; set; }

        public abstract void AddPropertyChangedListener(PropertyChangedEventHandler handler);

        public abstract void setVelocity(int velocityX, int velocityY);

        public abstract int Diameter { get; }

        public abstract int VelocityX { get; }

        public abstract int VelocityY { get; }

        public abstract int Mass { get; }
        public abstract int Size { get; }
        public static ConcurrentQueue<AbstractBallDataAPI> BallQueue { get; } = new ConcurrentQueue<AbstractBallDataAPI>();

        public static AbstractBallDataAPI CreateInstance(Vector2 position, int deltaX, int deltaY, int radius, int mass, bool isRunning)
        {
            return new BallData(position, deltaX, deltaY, radius, mass, isRunning);
        }

    }
}
