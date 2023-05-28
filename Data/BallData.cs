using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Data
{
    internal class BallData : AbstractBallDataAPI, INotifyPropertyChanged
    {
        // Define BallData properties and assign them to BallDataAPI properties
        private Vector2 _position;
        private int _velocityX;
        private int _velocityY;
        private int _radius;
        private int _mass;

        private bool _isRunning;

        private int _x;
        private int _y;
        private Vector2 _updatedPosition;
        private double _velocity;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Constructor
        public BallData(Vector2 position, int velX, int velY, int radius, int mass, bool isRunning)
        {
            _position = position;
            _velocityX = velX;
            _velocityY = velY;
            _radius = radius;
            _mass = mass;
            this.isRunning = isRunning;

            Task.Run(() => Move());
        }

        public override Vector2 Position => _position;
        public override int PositionX => (int)_position.X;
        public override int PositionY => (int)_position.Y;
        public override int VelocityX { get => _velocityX; set => _velocityX = value; }
        public override int VelocityY { get => _velocityY; set => _velocityY = value; }
        public override int Diameter => _radius * 2;
        public override int Mass => _mass;
        public override int Radius => _radius;
        public override bool isRunning { get => _isRunning; set => _isRunning = value; }

        public override void setVelocity(int velocityX, int velocityY)
        {
            this._velocityX = velocityX;
            this._velocityY = velocityY;
        }

        private void setPosition(Vector2 position)
        {
            this._position.X = position.X;
            this._position.Y = position.Y;
        }

        private async Task Move()
        {
            while(true)
            {
                if (isRunning)
                {
                    _x = (int)_position.X + _velocityX;
                    _y = (int)_position.Y + _velocityY;
                    _updatedPosition = new Vector2(_x, _y);

                    setPosition(_updatedPosition);
                }
                _velocity = Math.Sqrt(_velocityX + _velocityX + _velocityY * _velocityY);
                await Task.Delay(TimeSpan.FromMilliseconds(1000 / 360 * _velocity));
            }
        }

        public override void AddPropertyChangedListener(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }
    }
}