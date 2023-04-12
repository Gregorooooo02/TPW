using Data;

namespace Logic
{
    internal class SimulationController : AbstractLogicAPI
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers;
        private AbstractDataAPI _data;
        private SimulationManager _simulationManager;
        private bool isRunning = false;

        public SimulationController(AbstractDataAPI? data = default)
        {
            _data = data ?? AbstractDataAPI.CreateInstance();
            _simulationManager = new SimulationManager(new Window(_data.WindowWidth, _data.WindowHeight), _data.BallDiameter);
            _observers = new HashSet<IObserver<IEnumerable<Ball>>>();
        }

        public override IEnumerable<Ball> Balls => _simulationManager.Balls;

        public override void SpawnBalls(int numberOfBalls)
        {
            _simulationManager.RandomizedBallSpawning(numberOfBalls);
        }
        public override void Simulation()
        {
            while (isRunning)
            {
                _simulationManager.ApplyForceToBalls();
                TrackBalls(Balls);
                Thread.Sleep(10);
            }
        }

        public override void StartSim()
        {
            if (!isRunning)
            {
                isRunning = true;
                Task.Run(Simulation);
            }
        }

        public override void StopSim()
        {
            if (isRunning)
            {
                isRunning = false;
            }
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<Ball>> observer)
        {
            _observers.Add(observer);

            return new SubscriptionController(_observers, observer);
        }

        private class SubscriptionController : IDisposable
        {
            private ISet<IObserver<IEnumerable<Ball>>> _observers;
            private IObserver<IEnumerable<Ball>> _observer;

            public SubscriptionController(ISet<IObserver<IEnumerable<Ball>>> observers,
                                    IObserver<IEnumerable<Ball>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers != null)
                {
                    _observers.Remove(_observer);
                }
            }
        }

        public void TrackBalls(IEnumerable<Ball> balls)
        {
            foreach (var observer in _observers)
            {
                if (balls is null)
                {
                    observer.OnError(new NullReferenceException("Ball is null!"));
                }
                else
                {
                    observer.OnNext(balls);
                }
            }
        }

        public void CompleteTracking()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }
            _observers.Clear();
        }
    }
}
