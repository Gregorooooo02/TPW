using Logic;

namespace Model
{
    public class BallModel
    {
        private Ball _ball;

        public int Diameter => _ball.Diameter;
        public int Radius => _ball.Radius;
        public Vector2 Position => CalcOffsetPosition(_ball.Position);
        public Vector2 Velocity => _ball.Velocity;

        public BallModel(Ball ball)
        {
            _ball = ball;
        }

        private Vector2 CalcOffsetPosition(Vector2 position)
        {
            return new Vector2(position.X - Radius, position.Y - Radius);
        }
    }
}