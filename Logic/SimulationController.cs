using Data;

namespace Logic
{
    internal class SimulationController : AbstractLogicAPI
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers; // A set of IObserver objects that will receive updates when the state of the simulation changes.
        private AbstractDataAPI _data; // A reference to an AbstractDataAPI object that provides simulation data.
        private SimulationManager _simulationManager; // A SimulationManager object that manages the simulation.
        private bool isRunning = false; // A boolean that indicates whether the simulation is running or not.

        public SimulationController(AbstractDataAPI? data = default)
        {
            _data = data ?? AbstractDataAPI.CreateInstance(); // If a data object is not provided, create a default one.
            _simulationManager = new SimulationManager(new Window(_data.WindowWidth, _data.WindowHeight), _data.BallDiameter); // Create a new SimulationManager object with the window and ball diameter provided by the data object.
            _observers = new HashSet<IObserver<IEnumerable<Ball>>>(); // Initialize the set of observers.
        }

        public override IEnumerable<Ball> Balls => _simulationManager.Balls; // Implement the Balls property from the AbstractLogicAPI interface.

        public override void SpawnBalls(int numberOfBalls)
        {
            _simulationManager.RandomizedBallSpawning(numberOfBalls); // Spawn a number of balls randomly in the simulation.
        }

        public override void Simulation()
        {
            while (isRunning)
            {
                _simulationManager.ApplyForceToBalls(); // Apply forces to the balls in the simulation.
                TrackBalls(Balls); // Update all the observers with the new state of the balls in the simulation.
                Thread.Sleep(10); // Wait for 10 milliseconds.
            }
        }

        public override void StartSim()
        {
            if (!isRunning)
            {
                isRunning = true; // Set the isRunning flag to true.
                Task.Run(Simulation); // Start a new Task that runs the Simulation method asynchronously.
            }
        }

        public override void StopSim()
        {
            if (isRunning)
            {
                isRunning = false; // Set the isRunning flag to false.
            }
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<Ball>> observer)
        {
            _observers.Add(observer); // Add a new observer to the set of observers.

            return new SubscriptionController(_observers, observer); // Return a new SubscriptionController object that manages the observer's subscription.
        }

        private class SubscriptionController : IDisposable
        {
            private ISet<IObserver<IEnumerable<Ball>>> _observers; // A set of observers that are subscribed to the simulation.
            private IObserver<IEnumerable<Ball>> _observer; // A reference to the observer that is being managed by this SubscriptionController.

            public SubscriptionController(ISet<IObserver<IEnumerable<Ball>>> observers, IObserver<IEnumerable<Ball>> observer)
            {
                _observers = observers; // Set the set of observers.
                _observer = observer; // Set the observer being managed.
            }

            public void Dispose()
            {
                if (_observers != null)
                {
                    _observers.Remove(_observer); // Remove the observer being managed from the set of observers.
                }
            }
        }

        public void TrackBalls(IEnumerable<Ball> balls)
        {
            foreach (var observer in _observers)
            {
                if (balls is null)
                {
                    observer.OnError(new NullReferenceException("Ball is null!")); // Notify the observers with an error if the balls collection is null.
                }
                else
                {
                    observer.OnNext(balls); // If the balls collection is not null, it notifies the observer with the collection by calling OnNext.
                }
            }
        }

        public void CompleteTracking()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer)) // It checks if the observer is still in the observer collection before calling OnCompleted to ensure that it hasn't already been disposed of.
                {
                    observer.OnCompleted(); // It notifies each observer by calling OnCompleted() to signal that the observable sequence has completed and won't produce any more elements.
                }
            }
            _observers.Clear(); // After notifying all observers, it clears the observer collection.
        }
    }
}
