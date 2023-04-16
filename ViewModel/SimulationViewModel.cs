using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    // This class represents the ViewModel for the simulation View.
    // It implements the IObserver interface to receive notifications from the Model when the collection of BallModel objects changes.
    public class SimulationViewModel : ViewModel, IObserver<IEnumerable<BallModel>>
    {
        private IDisposable? _unsubscriber;
        private ObservableCollection<BallModel> _balls;
        private AbstractModelAPI _logic;
        private int _ballCount;
        private bool _isSimulationRunning = false;

        // This property returns the collection of BallModel objects that are currently displayed in the UI.
        public IEnumerable<BallModel> Balls { get { return _balls; } }

        // These properties represent the Start and Stop buttons in the UI and the commands that are executed when they are clicked.
        public ICommand StartSimulationInput { get; init; }
        public ICommand StopSimulationInput { get; init; }

        public SimulationViewModel(AbstractModelAPI? model = default) : base()
        {
            // If no AbstractModelAPI object is provided, create a new one using the CreateInstance method.
            _logic = model ?? AbstractModelAPI.CreateInstance();
            _balls = new ObservableCollection<BallModel>();

            // Initialize the Start and Stop commands with new instances of the respective input classes.
            StartSimulationInput = new StartSimulationInput(this);
            StopSimulationInput = new StopSimulationInput(this);

            // Subscribe to notifications from the Model.
            Subscriber(_logic);
        }

        // This property represents the number of BallModel objects that should be created when the simulation is started.
        public int BallsCount
        {
            get => _ballCount;
            set { SetField(ref _ballCount, value); }
        }

        // This property represents whether the simulation is currently running or not.
        public bool IsSimulationRunning
        {
            get { return _isSimulationRunning; }
            private set { SetField(ref _isSimulationRunning, value); }
        }

        // This method starts the simulation.
        public void StartSimulation()
        {
            IsSimulationRunning = true;
            _logic.SpawnBalls(BallsCount);
            _logic.Start();
        }

        // This method stops the simulation.
        public void StopSimulation()
        {
            IsSimulationRunning = false;
            _logic.Stop();
        }

        #region Observer
        // This method is called when the ViewModel is subscribed to the Model's notifications.
        public void Subscriber(IObservable<IEnumerable<BallModel>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        // This method is called when the Model has finished sending notifications.
        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        // This method is called when an error occurs while the Model is sending notifications.
        public void OnError(Exception error)
        {
            throw error;
        }

        // This method is called when the collection of BallModel objects in the Model changes.
        public void OnNext(IEnumerable<BallModel> balls)
        {
            if (balls is null)
            {
                balls = new List<BallModel>();
            }

            // Update the collection of BallModel objects in the ViewModel and notify the UI that the Balls property has changed.
            _balls = new ObservableCollection<BallModel>(balls);
            OnPropertyChanged(nameof(Balls));
        }
        #endregion
    }
}
