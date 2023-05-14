using Data;

namespace Logic
{

    public class Ball : IBall, IEquatable<Ball>
    {
        private readonly object positionLock = new();
        private readonly object velocityLock = new();

        public int Diameter { get; init; }
        public int Radius { get; init; }


        public Vector2 Velocity
        {
            get
            {
                lock (velocityLock)
                {
                    return _velocity;
                }
            }
            set
            {
                lock (velocityLock)
                {
                    _velocity = value;
                }
            }
        }
        public Vector2 Position
        {
            get
            {
                lock (positionLock)
                {
                    return _position;
                }
            }
            private set
            {
                lock (positionLock)
                {
                    if (_position == value) return;
                    _position = value;
                    TrackBall(this);
                }
            }
        }
        private readonly ISet<IObserver<IBall>> _observers;
        private readonly Window _board;
        private readonly IBallData _ballDto;

        private IDisposable? _disposer;
        private IDisposable? _unsubscriber;
        private Vector2 _velocity;
        private Vector2 _position;

        // Constructors:
        public Ball(int diameter, Vector2 position, Vector2 speed, Window board, IBallData? ballDto = default)
        {
            Diameter = diameter;
            Position = position;
            Velocity = speed;
            Radius = diameter / 2;
            _board = board;
            _ballDto = ballDto ?? new BallData(Diameter, Position.X, Position.Y, Velocity.X, Velocity.Y);

            _observers = new HashSet<IObserver<IBall>>();
            _disposer = ThreadManager.Add<float>(Move);
            Follow(_ballDto);
        }
        public Ball(int diameter, int posX, int posY, float speedX, float speedY, Window board, IBallData? ballDto = default)
      : this(diameter, new Vector2(posX, posY), new Vector2(speedX, speedY), board, ballDto)
        { }

        public void Move(float delta)
        {
            if (Velocity.IsZero()) return;

            float strength = (delta * 0.01f).Clamp(0f, 1f);

            var move = Velocity * strength;
            var (posX, posY) = Position;
            var (newSpeedX, newSpeedY) = Velocity;

            var (boundryXx, boundryXy) = _board.XBoundry;
            if (!posX.Between(boundryXx, boundryXy, Radius))
            {
                if (posX <= boundryXx + Radius)
                {
                    newSpeedX = MathF.Abs(newSpeedX);
                }
                else
                {
                    newSpeedX = -MathF.Abs(newSpeedX);
                }
            }
            var (boundryYx, boundryYy) = _board.YBoundry;
            if (!posY.Between(boundryYx, boundryYy, Radius))
            {
                if (posY <= boundryYx + Radius)
                {
                    newSpeedY = MathF.Abs(newSpeedY);
                }
                else
                {
                    newSpeedY = -MathF.Abs(newSpeedY);
                }
            }

            _ballDto?.SetSpeed(newSpeedX, newSpeedY);
            _ballDto?.Move(move.X, move.Y);
        }

        public bool Touches(IBall ball)
        {
            int minDistance = this.Radius + ball.Radius;
            float minDistanceSquared = minDistance * minDistance;
            float actualDistanceSquared = Vector2.DistanceSquared(this.Position, ball.Position);

            return minDistanceSquared >= actualDistanceSquared;
        }

        public void Follow(IObservable<IBallData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        public void OnError(Exception error) => throw error;

        public void OnNext(IBallData ballDto)
        {
            Position = new Vector2(ballDto.PositionX, ballDto.PositionY);
            Velocity = new Vector2(ballDto.SpeedX, ballDto.SpeedY);
        }

        public IDisposable Subscribe(IObserver<IBall> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void TrackBall(IBall ball)
        {
            if (_observers is null) return;
            foreach (var observer in _observers)
            {
                observer.OnNext(ball);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<IBall>> _observers;
            private readonly IObserver<IBall> _observer;

            public Unsubscriber(ISet<IObserver<IBall>> observers, IObserver<IBall> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _disposer?.Dispose();
        }

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

        public override string? ToString()
        {
            return $"Ball d={Diameter}, P=[{Position.X:n1}, {Position.Y:n1}], S=[{Velocity.X:n1}, {Velocity.Y:n1}]";
        }

    }
}