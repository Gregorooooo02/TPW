using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class SimulationViewModel : ViewModel, IObserver<IEnumerable<BallModel>>
    {
        private IDisposable? _unsubscriber;
        private ObservableCollection<BallModel> _balls;
        private AbstractModelAPI _logic;
        private int _ballCount;
        private bool _isSimulationRunning = false;

        public IEnumerable<BallModel> Balls { get { return _balls; } }
        public ICommand StartSimulationInput { get; init; }
        public ICommand StopSimulationInput { get; init; }

        public SimulationViewModel(AbstractModelAPI? model = default) : base()
        {
            _logic = model ?? AbstractModelAPI.CreateInstance();
            _balls = new ObservableCollection<BallModel>();

            StartSimulationInput = new StartSimulationInput(this);
            StopSimulationInput = new StopSimulationInput(this);
            Subscriber(_logic);
        }

        public int BallsCount
        {
            get => _ballCount;
            set { SetField(ref _ballCount, value); }
        }

        public bool IsSimulationRunning
        {
            get { return _isSimulationRunning; }
            private set { SetField(ref _isSimulationRunning, value); }
        }

        public void StartSimulation()
        {
            IsSimulationRunning = true;
            _logic.SpawnBalls(BallsCount);
            _logic.Start();
        }

        public void StopSimulation()
        {
            IsSimulationRunning = false;
            _logic.Stop();
        }

        #region Observer
        public void Subscriber(IObservable<IEnumerable<BallModel>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(IEnumerable<BallModel> balls)
        {
            if (balls is null)
            {
                balls = new List<BallModel>();
            }
            _balls = new ObservableCollection<BallModel>(balls);
            OnPropertyChanged(nameof(Balls));
        }
        #endregion
    }
}
