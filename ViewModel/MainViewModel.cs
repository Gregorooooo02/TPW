namespace ViewModel
{
    public class MainViewModel : ViewModel
    {
        public ViewModel ViewModel { get; }

        public MainViewModel() : base()
        {
            ViewModel = new SimulationViewModel();
        }
    }
}