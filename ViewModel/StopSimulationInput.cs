using System.ComponentModel;

namespace ViewModel
{
    public class StopSimulationInput : Command
    {
        private SimulationViewModel _simulationViewModel;

        public StopSimulationInput(SimulationViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;
            _simulationViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && _simulationViewModel.IsSimulationRunning;
        }

        public override void Execute(object? parameter)
        {
            _simulationViewModel.StopSimulation();
        }

        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simulationViewModel.IsSimulationRunning))
            {
                OnExecuteChange();
            }
        }
    }
}
