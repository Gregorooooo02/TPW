﻿using Data;
using System.Diagnostics;

namespace Logic
{
    internal class Logic : AbstractLogicAPI
    {
        private readonly IList<IBall> _balls;
        private readonly ISet<IObserver<IBall>> _observers;
        private readonly AbstractDataAPI _data;
        private readonly Window _board;
        private readonly Random _rand = new();

        public Logic(AbstractDataAPI? data = default)
        {
            _data = data ?? AbstractDataAPI.CreateInstance();
            _observers = new HashSet<IObserver<IBall>>();

            _board = new Window(_data.WindowWidth, _data.WindowHeight);
            _balls = new List<IBall>();
        }

        public override IEnumerable<IBall> CreateBalls(int count)
        {
            for (var i = 0; i < count; i++)
            {
                int diameter = GetRandomDiameter();
                Vector2 position = GetRandomPos(diameter);
                Vector2 speed = GetRandomSpeed();
                Ball newBall = new(diameter, position, speed, _board);
                _balls.Add(newBall);

                TrackBall(newBall);
            }
            ThreadManager.SetValidator(HandleCollisions);
            ThreadManager.Start();

            return _balls;
        }

        private Vector2 GetRandomPos(int diameter)
        {
            int radius = diameter / 2;
            int x = _rand.Next(radius, _board.Width - radius);
            int y = _rand.Next(radius, _board.Height - radius);
            return new Vector2(x, y);
        }

        private Vector2 GetRandomSpeed()
        {
            double x = (_rand.NextDouble() * 2.0 - 1.0) * _data.MaxSpeed;
            double y = (_rand.NextDouble() * 2.0 - 1.0) * _data.MaxSpeed;
            return new Vector2((float)x, (float)y);
        }

        private int GetRandomDiameter()
        {
            return _rand.Next(_data.MinDiameter, _data.MaxDiameter + 1);
        }

        #region Provider

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void TrackBall(IBall ball)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(ball);
            }
        }

        private void EndTransmission()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }

        private void HandleCollisions()
        {
            var collisions = Collisions.Get(_balls);
            if (collisions.Count > 0)
            {
                foreach (var col in collisions)
                {
                    var (ball1, ball2) = col;
                    (ball1.Velocity, ball2.Velocity) = Collisions.CalculateSpeeds(ball1, ball2);
                }
            }
            Thread.Sleep(1);
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

        #endregion

        public override void Dispose()
        {
            EndTransmission();
            ThreadManager.Stop();

            Trace.WriteLine($"Average Delta = {ThreadManager.AverageDeltaTime}");
            Trace.WriteLine($"Average Fps = {ThreadManager.AverageFps}");
            Trace.WriteLine($"Total Frame Count = {ThreadManager.FrameCount}");

            foreach (var ball in _balls)
            {
                ball.Dispose();
            }
            _balls.Clear();
        }
    }

}