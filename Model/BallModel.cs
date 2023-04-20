using Logic; // Importing the namespace Logic, which contains the Ball class.

namespace Model
{
    internal class BallModel : IBallModel
    {
        private IBall _ball; // Private field to store the Ball object.
        private Vector2 _velocity => _ball.Velocity;
        private Vector2 _position => CalcOffsetPosition(_ball.Position);
        private int _radius => _ball.Radius;
        private int _diameter => _ball.Diameter;

        #region IBallModel
        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public int Radius
        {
            get { return _radius; }
        }

        public int Diameter
        {
            get { return _diameter; }
        }
        #endregion

        // Constructor that takes a Ball object and initializes the private field.
        public BallModel(IBall ball)
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
