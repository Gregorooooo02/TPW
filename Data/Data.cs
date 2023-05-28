using System.Numerics;

namespace Data
{
    // Define the Data class as internal and subclass AbstractDataAPI.
    internal class Data : AbstractDataAPI
    {
        // Define properties for Data class
        private int _windowHeight;
        private int _windowWidth;

        public Data(int windowHeight, int windowWidth)
        {
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
        }

        public override int getWindowHeight()
        {
            return _windowHeight;
        }

        public override int getWindowWidth()
        {
            return _windowWidth;
        }

        public override AbstractBallDataAPI spawnBalls(bool isRunning)
        {
            Random _rand = new Random();
            int _positionX = _rand.Next(20, _windowWidth - 20);
            int _positionY = _rand.Next(20, _windowHeight - 20);
            Vector2 _position = new Vector2((int)_positionX, (int)_positionY);

            int _velocityX = _rand.Next(-3, 4);
            int _velocityY = _rand.Next(-3, 4);

            if (_velocityX == 0)
            {
                _velocityX = _rand.Next(1, 3) * 2 - 3;
            }
            if (_velocityY == 0)
            {
                _velocityY = _rand.Next(1, 3) * 2 - 3;
            }

            int _ballRadius = 15;
            int _ballMass = 200;

            return AbstractBallDataAPI.CreateInstance(_position, _velocityX, _velocityY, _ballRadius, _ballMass, isRunning);
        }
    }
}
