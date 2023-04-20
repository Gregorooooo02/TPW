namespace Logic
{
    internal class SimulationManager
    {
        private Window _window; // The window in which the simulation takes place.
        private int _ballDiameter; // The diameter of the balls.
        private int _ballRadius; // The radius of the balls (half the diameter).
        private const float _maxVelocity = 50; // The maximum velocity of the balls.
        private Random _random; // A random number generator.

        public IList<Ball> Balls { get; private set; } // A list of balls in the simulation.

        public SimulationManager(Window window, int ballDiameter)
        {
            _window = window; // Store the window in which the simulation takes place.
            _ballDiameter = ballDiameter; // Store the diameter of the balls.
            _ballRadius = ballDiameter / 2; // Calculate the radius of the balls.
            _random = new Random(); // Initialize the random number generator.
            Balls = new List<Ball>(); // Create a new list to store the balls in.
        }

        public void ApplyForceToBalls(float force = 0.1f)
        {
            foreach (var ball in Balls)
            {
                ball.Move(_window.XBoundry, _window.YBoundry, force); // Apply a force to each ball in the simulation.
            }
        }

        private Vector2 RandomizedBallPosition()
        {
            int x = _random.Next(_ballRadius, _window.Width - _ballRadius); // Generate a random x-coordinate within the bounds of the window.
            int y = _random.Next(_ballRadius, _window.Height - _ballRadius); // Generate a random y-coordinate within the bounds of the window.

            return new Vector2(x, y); // Return a vector representing the randomized ball position.
        }

        private Vector2 RandomizedBallVelocity()
        {
            const float _halfVelocity = _maxVelocity / 2; // Calculate half of the maximum velocity.

            double x = _random.NextDouble() * _maxVelocity - _halfVelocity; // Generate a random x-velocity between -25 and 25.
            double y = _random.NextDouble() * _maxVelocity - _halfVelocity; // Generate a random y-velocity between -25 and 25.

            return new Vector2((float)x, (float)y); // Return a vector representing the randomized ball velocity.
        }

        public IList<Ball> RandomizedBallSpawning(int numberOfBalls)
        {
            Balls = new List<Ball>(numberOfBalls); // Create a new list to store the balls in, with a capacity of numberOfBalls.

            for (int i = 0; i < numberOfBalls; i++)
            {
                Vector2 _position = RandomizedBallPosition(); // Generate a randomized ball position.
                Vector2 _velocity = RandomizedBallVelocity(); // Generate a randomized ball velocity.

                Balls.Add(new Ball(_velocity, _position, _ballDiameter)); // Create a new ball with the randomized velocity, position, and diameter, and add it to the list of balls.
            }

            return Balls; // Return the list of balls.
        }
    }
}
