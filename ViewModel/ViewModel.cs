using Model;

namespace ViewModel
{
    public class MainViewModel : AbstractViewModelAPI
    {
        public AbstractViewModelAPI CurrentViewModel { get; }

        public MainViewModel()
            : base()
        {
            CurrentViewModel = new SimulationViewModel(
                ballsCountValidator: new BallsCountValidator(1, 30)
                );
        }
    }
}
