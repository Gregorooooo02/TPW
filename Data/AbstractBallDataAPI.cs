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
        public abstract int Size { get; }

        // Setter for velocity property
        public abstract void setVelocity(int velocityX,  int velocityY);
        // 
        public abstract void AddPropertyChangedListener(PropertyChangedEventHandler handler);

        public static AbstractBallDataAPI CreateInstance(Vector2 position,  int velX, int velY, int size, int mass, bool isRunning)
        {
            return new BallData();
        }
    }
}
