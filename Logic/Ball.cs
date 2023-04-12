using System.Numerics;

namespace Logic
{
    public class Ball : IEquatable<Ball>
    {
        public Vector2 Velocity { get; private set; }
        public Vector2 Position { get; private set; }
        public int Radius { get; init; }
        public int Diameter { get; init; }

        public Ball(float xVel, float yVel, int x, int y, int diameter)
            : this(new Vector2(xVel, yVel), new Vector2(x, y), diameter) { }

        public Ball(Vector2 velocity, Vector2 position, int diameter)
        {
            Velocity = velocity;
            Position = position;
            Diameter = diameter;
            Radius = diameter / 2;
        }

        public void Move(Vector2 xBorder, Vector2 yBorder, float force = 1f)
        {
            if (Velocity.IsZero())
                return;
            Position += Velocity * force;

            var (X, Y) = Position;
            if (!X.CheckBoundry(xBorder.X, xBorder.Y, Radius))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            if (!Y.CheckBoundry(yBorder.X, yBorder.Y, Radius))
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
        }

        public Vector2 IncreaseVelocity(Vector2 velocity)
        {
            Velocity += velocity;
            return Velocity;
        }

        public override bool Equals(object? obj)
        {
            return obj is Ball ball
                && Equals(ball);
        }

        public bool Equals(Ball? other)
        {
            return other is not null
                && Velocity == other.Velocity
                && Position == other.Position
                && Diameter == other.Diameter;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Velocity, Position, Diameter);
        }
    }

    public static class Boundry
    {
        public static bool CheckBoundry(this float value, float min, float max, float padding = 0f)
        {
            return (value - padding >= min) && (value + padding <= max);
        }

        public static bool CheckBoundry(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}
