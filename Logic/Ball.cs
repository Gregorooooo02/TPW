using Data;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;

namespace Logic
{
    internal class Ball : AbstractBallAPI
    {
        public override int WindowHeight { get; }
        public override int WindowWidth { get; }
        public override List<AbstractBallDataAPI> BallsList { get; }
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private AbstractDataAPI dataAPI;

        private int _velocityX;
        private int _velocityY;
        private Vector2 _position;

        public Ball(AbstractDataAPI dataAPI)
        {
            this.WindowHeight = dataAPI.getWindowHeight();
            this.WindowWidth = dataAPI.getWindowWidth();
            BallsList = new List<AbstractBallDataAPI>();
            this.dataAPI = dataAPI;
        }

        public override int GetNumberOfBalls()
        {
            return BallsList.Count;
        }

        public override int GetPositionX(int ballIndex)
        {
            if (ballIndex < BallsList.Count && ballIndex >= 0)
            {
                return (int)BallsList[ballIndex].Position.X;
            }
            else
            {
                return -1;
            }
        }

        public override int GetPositionY(int ballIndex)
        {
            if (ballIndex < BallsList.Count && ballIndex >= 0)
            {
                return (int)BallsList[ballIndex].Position.Y;
            }
            else
            {
                return -1;
            }
        }

        public override int GetRadius(int ballIndex)
        {
            if (ballIndex < BallsList.Count && ballIndex >= 0)
            {
                return BallsList[ballIndex].Radius;
            }
            else
            {
                return -1;
            }
        }

        public override void SpawnBall()
        {
            AbstractBallDataAPI _ball = dataAPI.spawnBalls(true);

            BallsList.Add(_ball);
            _ball.AddPropertyChangedListener(CollisionCheck);
        }

        public override void StartSimulation()
        {
            foreach (var _ball in BallsList)
            {
                _ball.isRunning = true;
            }
        }

        public override void StopSimulation()
        {
            foreach (var _ball in BallsList)
            {
                _ball.isRunning = false;
            }
        }

        private void CollideWindow(AbstractBallDataAPI _ball)
        {
            _lock.EnterWriteLock();

            try
            {
                _velocityX = _ball.VelocityX;
                _velocityY = _ball.VelocityY;
                _position = _ball.Position;

                if (_position.X + _ball.VelocityX < 0 || _position.X + _ball.VelocityX >= WindowWidth)
                {
                    _velocityX = -_velocityX;
                }

                if (_position.Y + _ball.VelocityY < 0 || _position.Y + _ball.VelocityY >= WindowHeight)
                {
                    _velocityY = -_velocityY;
                }

                _ball.setVelocity(_velocityX, _velocityY);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private bool CollideBallWithBall(AbstractBallDataAPI _firstBall, AbstractBallDataAPI _secondBall)
        {
            Vector2 _position1 = _firstBall.Position;
            Vector2 _position2 = _secondBall.Position;

            int distance = (int)Math.Sqrt(Math.Pow((_position1.X + _firstBall.VelocityX) - (_position2.X + _secondBall.VelocityX), 2) + Math.Pow((_position1.Y + _firstBall.VelocityY) - (_position2.Y + _secondBall.VelocityY), 2));

            if (distance <= _firstBall.Radius / 2 + _secondBall.Radius / 2)
            {
                _lock.EnterWriteLock();

                try
                {
                    int _firstVelX = _firstBall.VelocityX;
                    int _firstVelY = _firstBall.VelocityY;
                    int _secondVelX = _secondBall.VelocityX;
                    int _secondVelY = _secondBall.VelocityY;

                    int _updatedFirstVX, _updatedFirstVY;
                    int _updatedSecondVX, _updatedSecondVY;

                    _updatedFirstVX = (_firstVelX * (_firstBall.Mass - _secondBall.Mass) + 2 * _secondBall.Mass * _secondVelX) / (_firstBall.Mass + _secondBall.Mass);
                    _updatedFirstVY = (_firstVelY * (_firstBall.Mass - _secondBall.Mass) + 2 * _secondBall.Mass * _secondVelY) / (_firstBall.Mass + _secondBall.Mass);
                    _updatedSecondVX = (_secondVelX * (_secondBall.Mass - _firstBall.Mass) + 2 * _firstBall.Mass * _firstVelX) / (_firstBall.Mass + _secondBall.Mass);
                    _updatedSecondVY = (_secondVelY * (_secondBall.Mass - _firstBall.Mass) + 2 * _firstBall.Mass * _firstVelY) / (_firstBall.Mass + _secondBall.Mass);

                    _firstBall.setVelocity(_updatedFirstVX, _updatedFirstVY);
                    _secondBall.setVelocity(_updatedSecondVX, _updatedSecondVY);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
                return false;
            }
            return true;
        }

        private void CollisionCheck(object sender, PropertyChangedEventArgs e)
        {
            AbstractBallDataAPI _ball = (AbstractBallDataAPI)sender;

            if (_ball != null)
            {
                CollideWindow(_ball);

                foreach (var _secondBall in BallsList)
                {
                    if (!_secondBall.Equals(_ball))
                    {
                        CollideBallWithBall(_ball, _secondBall);
                    }
                }
            }
        }
    }
}