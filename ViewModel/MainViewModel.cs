using Model;

namespace ViewModel
{
    public class MainViewModel : ViewModel
    {
        public ViewModel CurrentViewModel { get; }

        public MainViewModel()
            : base()
        {
            CurrentViewModel = new SimulationViewModel(
                ballsCountValidator: new BallsCountValidator(1, 30)
                );
        }
    }
}
