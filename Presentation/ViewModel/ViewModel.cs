using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    internal class ViewModel : AbstractViewModelAPI
    {
        private readonly AbstractModelAPI _model;
        private ObservableCollection<object> _balls;
        private int _numberOfBalls;
        public override ICommand StartCommand { get; }
        public override ICommand StopCommand { get; }
        public override ICommand CreateBallCommand { get; }

        public ViewModel(int boardWidth, int boardHeight)
        {
            _model = AbstractModelAPI.CreateModelAPI(boardWidth, boardHeight, null);
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
            CreateBallCommand = new Command(CreateBall);
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

        public override void Start()
        {
            _model.Start();
        }

        public override void Stop()
        {
            _model.Stop();
        }

        public override void CreateBall()
        {
            int maxBalls = 10;

            // Calculate the current total number of balls on the board
            int currentBallsCount = Balls.Count;

            // Calculate the remaining number of balls that can be generated within the limit
            int remainingBalls = Math.Max(0, maxBalls - currentBallsCount);

            // Calculate the number of balls to generate based on the remaining limit and the entered value
            int numberOfBalls = Math.Min(NumberOfBalls, remainingBalls);

            for (int i = 0; i < numberOfBalls; i++)
            {
                _model.CreateBall();
            }

            Balls = GetBalls();
        }

        public override ObservableCollection<object> GetBalls()
        {
            return _model.GetBalls();
        }
    }
}
