using Logic;

namespace Model
{
    // This class represents a concrete implementation of AbstractModelAPI and provides the functionality of 
    // tracking and simulating balls. It acts as both an observer of a provider of balls and an observable 
    // provider of BallModels to its observers.
    public class Model : AbstractModelAPI
    {
        private AbstractLogicAPI _logic;  // A reference to an instance of AbstractLogicAPI for the Model to use
        private ISet<IObserver<IEnumerable<BallModel>>> _observers; // A set of observers that are tracking the BallModels 
        private IDisposable? _unsubscriber; // An object that represents the subscription between the Model and the Observable

        // Constructor that creates an instance of Model with a reference to an AbstractLogicAPI object
        public Model(AbstractLogicAPI? logic = default)
        {
            _logic = logic ?? AbstractLogicAPI.CreateInstance(); // If logic is null, create a new instance of AbstractLogicAPI
            _observers = new HashSet<IObserver<IEnumerable<BallModel>>>(); // Create an empty hashset of observers 
            Subscribe(_logic); // Subscribe the Model to the Observable
        }

        // Method to spawn balls using the AbstractLogicAPI object
        public override void SpawnBalls(int numberOfBalls)
        {
            _logic.SpawnBalls(numberOfBalls);
        }

        // Method to start the simulation using the AbstractLogicAPI object
        public override void Start()
        {
            _logic.StartSim();
        }

        // Method to stop the simulation using the AbstractLogicAPI object
        public override void Stop()
        {
            _logic.StopSim();
        }

        // Method to convert a collection of Ball objects to BallModel objects
        public static IEnumerable<BallModel> BallToBallModel(IEnumerable<Ball> balls)
        {
            return balls.Select(ball => new BallModel(ball));
        }

        #region Observer
        // Method to subscribe the Model to an IObservable<IEnumerable<Ball>> object
        public void Subscribe(IObservable<IEnumerable<Ball>> provider)
        {
            _unsubscriber = provider.Subscribe(this); // Subscribe the Model to the provider
        }

        // Method called when the Observable is completed
        public override void OnCompleted()
        {
            _unsubscriber?.Dispose(); // Dispose the subscription object
        }

        // Method called when the Observable encounters an error
        public override void OnError(Exception error)
        {
            throw error;
        }

        // Method called when the Observable sends a collection of Ball objects
        public override void OnNext(IEnumerable<Ball> balls)
        {
            TrackBalls(BallToBallModel(balls)); // Convert the Ball objects to BallModel objects and track them
        }
        #endregion

        #region Provider
        // Method to subscribe an IObserver<IEnumerable<BallModel>> object to the Model
        public override IDisposable Subscribe(IObserver<IEnumerable<BallModel>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer); // Add the observer to the set of observers
            }

            return new SubscriptionController(_observers, observer); // Return a SubscriptionController object that represents the subscription
        }

        // Class that controls the subscription between the Model and its observers
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
            // Iterate through all subscribed observers
            foreach (var observer in _observers)
            {
                // If the collection of balls is null, throw an exception
                if (balls is null)
                {
                    observer.OnError(new NullReferenceException("Ball is null!"));
                }
                // Otherwise, call the observer's OnNext method with the collection of balls
                else
                {
                    observer.OnNext(balls);
                }
            }
        }

        public void CompleteTracking()
        {
            // Iterate through all subscribed observers
            foreach (var observer in _observers)
            {
                // If the current observer is in the set of observers, call its OnCompleted method
                if (_observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }

            // Clear the set of observers
            _observers.Clear();
        }

        #endregion
    }
}
