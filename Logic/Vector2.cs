namespace Logic
{
    public struct Vector2 : IEquatable<Vector2>
    {
        // X and Y properties that allow getting and setting the X and Y values of a Vector2
        public float X { get; set; }
        public float Y { get; set; }

        // Constructor that creates a new Vector2 with the given X and Y values
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        // Calculates and returns the distance between two Vector2s using the DistanceSquared method and square root
        public static float Distance(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt(DistanceSquared(point1, point2));
        }

        // Calculates and returns the squared distance between two Vector2s
        public static float DistanceSquared(Vector2 point1, Vector2 point2)
        {
            float xDifference = point1.X - point2.X;
            float yDifference = point1.Y - point2.Y;
            return xDifference * xDifference + yDifference * yDifference;
        }

        // Checks if the Vector2 is (0,0)
        public bool IsZero()
        {
            return Equals(new Vector2(0, 0));
        }

        // Deconstructs the Vector2 into its X and Y components
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        // Checks if the Vector2 is equal to another object
        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector
                && Equals(vector);
        }

        // Checks if the Vector2 is equal to another Vector2
        public bool Equals(Vector2 other)
        {
            float xDiff = X - other.X;
            float yDiff = Y - other.Y;
            return xDiff * xDiff + yDiff * yDiff < 9.99999944E-11f;
        }

        // Adds two Vector2s together
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X + rhs.X,
                Y = lhs.Y + rhs.Y,
            };
        }

        // Subtracts one Vector2 from another
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X - rhs.X,
                Y = lhs.Y - rhs.Y,
            };
        }

        // Multiplies two Vector2s together
        public static Vector2 operator *(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X * rhs.X,
                Y = lhs.Y * rhs.Y,
            };
        }

        // Divides one Vector2 by another
        public static Vector2 operator /(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X / rhs.X,
                Y = lhs.Y / rhs.Y,
            };
        }

        // Negates a Vector2
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2
            {
                X = -vector.X,
                Y = -vector.Y,
            };
        }

        // Multiplies a Vector2 by a scalar value
        public static Vector2 operator *(Vector2 lhs, float d)
        {
            return new Vector2
            {
                X = lhs.X * d,
                Y = lhs.Y * d,
            };
        }

        // Divides a Vector2 by a scalar value
        public static Vector2 operator /(Vector2 lhs, float d)
        {
            return new Vector2
            {
                X = lhs.X / d,
                Y = lhs.Y / d,
            };
        }

        // Overrides the equality operator to compare two Vector2s
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.Equals(rhs);
        }

        // Overrides the inequality operator to compare two Vector2s
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        // Overrides GetHashCode() to generate a hash code for the Vector2 object
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

    }
}
