using Logic;

namespace Model
{
    public class Model : AbstractModelAPI
    {
        private AbstractLogicAPI _logic;
        private ISet<IObserver<IEnumerable<BallModel>>> _observers;
        private IDisposable? _unsubscriber;

        public Model(AbstractLogicAPI? logic = default)
        {
            _logic = logic ?? AbstractLogicAPI.CreateInstance();
            _observers = new HashSet<IObserver<IEnumerable<BallModel>>>();
            Subscribe(_logic);
        }

        public override void SpawnBalls(int numberOfBalls)
        {
            _logic.SpawnBalls(numberOfBalls);
        }

        public override void Start()
        {
            _logic.StartSim();
        }

        public override void Stop()
        {
            _logic.StopSim();
        }

        public static IEnumerable<BallModel> BallToBallModel(IEnumerable<Ball> balls)
        {
            return balls.Select(ball => new BallModel(ball));
        }

        #region Observer
        public void Subscribe(IObservable<IEnumerable<Ball>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public override void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        public override void OnError(Exception error)
        {
            throw error;
        }

        public override void OnNext(IEnumerable<Ball> balls)
        {
            TrackBalls(BallToBallModel(balls));
        }
        #endregion

        #region Provider
        public override IDisposable Subscribe(IObserver<IEnumerable<BallModel>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new SubscriptionController(_observers, observer);
        }

        private class SubscriptionController : IDisposable
        {
            private ISet<IObserver<IEnumerable<BallModel>>> _observers;
            private IObserver<IEnumerable<BallModel>> _observer;

            public SubscriptionController(ISet<IObserver<IEnumerable<BallModel>>> observers,
                                    IObserver<IEnumerable<BallModel>> observer)
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

        public void TrackBalls(IEnumerable<BallModel> balls)
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
        #endregion
    }
}
