﻿using System.Numerics;

namespace Logic
{
    // Define the Ball class as public and implement the IEquatable interface for the Ball class.
    public class Ball : IEquatable<Ball>
    {
        // Define public properties for the ball's velocity, position, radius, and diameter.
        public Vector2 Velocity { get; private set; }
        public Vector2 Position { get; private set; }
        public int Radius { get; init; }
        public int Diameter { get; init; }

        // Define a constructor for creating a ball with a specified velocity, position, and diameter.
        public Ball(float xVel, float yVel, int x, int y, int diameter)
            : this(new Vector2(xVel, yVel), new Vector2(x, y), diameter) { }

        // Define a constructor for creating a ball with a specified velocity, position, and diameter.
        public Ball(Vector2 velocity, Vector2 position, int diameter)
        {
            Velocity = velocity;
            Position = position;
            Diameter = diameter;
            Radius = diameter / 2;
        }

        // Define a method for moving the ball within the specified x and y borders.
        public void Move(Vector2 xBorder, Vector2 yBorder, float force = 1f)
        {
            // If the ball has no velocity, do not move the ball.
            if (Velocity.IsZero())
                return;
            // Move the ball based on its velocity and force.
            Position += Velocity * force;

            // If the ball hits the left or right border, reverse its x velocity.
            var (X, Y) = Position;
            if (!X.CheckBoundry(xBorder.X, xBorder.Y, Radius))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            // If the ball hits the top or bottom border, reverse its y velocity.
            if (!Y.CheckBoundry(yBorder.X, yBorder.Y, Radius))
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
        }

        // Define a method for increasing the ball's velocity by a specified amount.
        public Vector2 IncreaseVelocity(Vector2 velocity)
        {
            Velocity += velocity;
            return Velocity;
        }

        // Override the Equals method to compare two Ball objects for equality.
        public override bool Equals(object? obj)
        {
            return obj is Ball ball
                && Equals(ball);
        }

        // Define the IEquatable.Equals method to compare two Ball objects for equality.
        public bool Equals(Ball? other)
        {
            return other is not null
                && Velocity == other.Velocity
                && Position == other.Position
                && Diameter == other.Diameter;
        }

        // Override the GetHashCode method to return a hash code for the Ball object.
        public override int GetHashCode()
        {
            return HashCode.Combine(Velocity, Position, Diameter);
        }
    }

    // Define a static class for checking the boundaries of a ball within the x and y borders.
    public static class Boundry
    {
        // Define an extension method for checking the boundary of a float value.
        public static bool CheckBoundry(this float value, float min, float max, float padding = 0f)
        {
            return (value - padding >= min) && (value + padding <= max);
        }

        // Define an extension method for checking the boundary of an int value.
        public static bool CheckBoundry(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}
