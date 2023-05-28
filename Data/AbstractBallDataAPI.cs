using System.ComponentModel;
using System.Numerics;

namespace Data
{
    public abstract class AbstractBallDataAPI
    {
        // Define BallData properties
        public abstract int PositionX { get; }
        public abstract int PositionY { get; }
        public abstract Vector2 Position { get; }
        public abstract int VelocityX { get; set; }
        public abstract int VelocityY { get; set; }
        public abstract int Diameter { get; }
        public abstract int Mass { get; }
        public abstract int Radius { get; }

        public abstract bool isRunning { get; set; }

        // Setter for velocity property
        public abstract void setVelocity(int velocityX,  int velocityY);
        public abstract void AddPropertyChangedListener(PropertyChangedEventHandler handler);

        public static AbstractBallDataAPI CreateInstance(Vector2 position,  int velX, int velY, int radius, int mass, bool isRunning)
        {
            return new BallData(position, velX, velY, radius, mass, isRunning);
        }
    }
}
