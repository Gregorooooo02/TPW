namespace Logic
{
    public class SimulationManager
    {
        private Window _window;
        private int _ballDiameter;
        private int _ballRadius;
        private const float _maxVelocity = 50;
        private Random _random;

        public IList<Ball> Balls { get; private set; }

        public SimulationManager(Window window, int ballDiameter)
        {
            _window = window;
            _ballDiameter = ballDiameter;
            _ballRadius = ballDiameter / 2;
            _random = new Random();
            Balls = new List<Ball>();
        }

        public void ApplyForceToBalls(float force = 0.1f)
        {
            foreach (var ball in Balls)
            {
                ball.Move(_window.XBoundry, _window.YBoundry, force);
            }
        }

        private Vector2 RandomizedBallPosition()
        {
            int x = _random.Next(_ballRadius, _window.Width - _ballRadius);
            int y = _random.Next(_ballRadius, _window.Height - _ballRadius);

            return new Vector2(x, y);
        }

        private Vector2 RandomizedBallVelocity()
        {
            const float _halfVelocity = _maxVelocity / 2;

            double x = _random.NextDouble() * _maxVelocity - _halfVelocity;
            double y = _random.NextDouble() * _maxVelocity - _halfVelocity;

            return new Vector2((float)x, (float)y);
        }

        public IList<Ball> RandomizedBallSpawning(int numberOfBalls)
        {
            Balls = new List<Ball>(numberOfBalls);

            for (int i = 0; i < numberOfBalls; i++)
            {
                Vector2 _position = RandomizedBallPosition();
                Vector2 _velocity = RandomizedBallVelocity();

                Balls.Add(new Ball(_velocity, _position, _ballDiameter));
            }

            return Balls;
        }
    }
}
