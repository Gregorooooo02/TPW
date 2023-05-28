using System.Numerics;

namespace Data
{
    // Define the Data class as internal and subclass AbstractDataAPI.
    internal class Data : AbstractDataAPI
    {
        // Define properties for Data class
        private int _windowHeight;
        private int _windowWidth;

        private int _positionX;
        private int _positionY;
        private Vector2 _position;

        private int _velocityX;
        private int _velocityY;

        private int _ballRadius = 15;
        private int _ballMass = 150;

        private Random _rand = new Random();

        public override int WindowHeight => _windowHeight;
        public override int WindowWidth => _windowWidth;

        public Data(int windowHeight, int windowWidth)
        {
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
        }

        public override AbstractBallDataAPI spawnBalls(bool isRunning)
        {
            _positionX = _rand.Next(20, _windowWidth - 20);
            _positionY = _rand.Next(20, _windowHeight - 20);
            _position = new Vector2(_positionX, _positionY);

            _velocityX = _rand.Next(-3, 5);
            _velocityY = _rand.Next(-3, 5);

            if (_velocityX == 0)
            {
                _velocityX = _rand.Next(1, 3) * 2 - 3;
            }
            if (_velocityY == 0)
            {
                _velocityY = _rand.Next(1, 3) * 2 - 3;
            }

            return AbstractBallDataAPI.CreateInstance(_position, _velocityX, _velocityY, _ballRadius, _ballMass, isRunning);
        }
    }
}
