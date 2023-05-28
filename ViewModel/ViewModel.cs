using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    internal class ViewModel : AbstractViewModelAPI
    {
        private readonly AbstractModelAPI _modelAPI;
        private ObservableCollection<object> _balls;
        private int _numberOfBalls;

        public override ICommand StartSimInput { get; }
        public override ICommand StopSimInput { get; }
        public override ICommand SpawnBallInput { get; }

        public ViewModel(int windowHeight, int windowWidth)
        {
            _modelAPI = AbstractModelAPI.CreateInstance(windowHeight, windowWidth, null);
            StartSimInput = new Command(StartSimulation);
            StopSimInput = new Command(StopSimulation);
            SpawnBallInput = new Command(SpawnBall);
            Balls = GetBalls();
        }

        public override ObservableCollection<object> Balls
        {
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                OnPropertyChanged();
            }
        }

        public override ObservableCollection<object> GetBalls()
        {
            return _modelAPI.GetBalls();
        }

        public override void SpawnBall()
        {
            int _maxNumberOfBalls = 10;
            int _currentBallsCount = Balls.Count;
            int _remainingBalls = Math.Max(0, _maxNumberOfBalls - _currentBallsCount);
            int _numberOfBalls = Math.Min(NumberOfBalls, _remainingBalls);

            for (int i = 0; i < _numberOfBalls; i++)
            {
                _modelAPI.SpawnBall();
            }

            Balls = GetBalls();
        }

        public override void StartSimulation()
        {
            _modelAPI.StartSimulation();
        }

        public override void StopSimulation()
        {
            _modelAPI.StopSimulation();
        }
    }
}
