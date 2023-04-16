using Logic; // Importing the namespace Logic, which contains the Ball class.

namespace Model
{
    public class BallModel
    {
        private Ball _ball; // Private field to store the Ball object.

        // Public property that returns the diameter of the Ball.
        public int Diameter => _ball.Diameter;

        // Public property that returns the radius of the Ball.
        public int Radius => _ball.Radius;

        // Public property that returns the position of the Ball, offset by the radius.
        public Vector2 Position => CalcOffsetPosition(_ball.Position);

        // Public property that returns the velocity of the Ball.
        public Vector2 Velocity => _ball.Velocity;

        // Constructor that takes a Ball object and initializes the private field.
        public BallModel(Ball ball)
        {
            _ball = ball;
        }

        // Private method that calculates the offset position of the Ball based on its radius.
        private Vector2 CalcOffsetPosition(Vector2 position)
        {
            return new Vector2(position.X - Radius, position.Y - Radius);
        }
    }
}
