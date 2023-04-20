using System.ComponentModel;

namespace ViewModel
{
    internal class StopSimulationInput : Command
    {
        private SimulationViewModel _simulationViewModel;

        public StopSimulationInput(SimulationViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;
            // Subscribe to the PropertyChanged event of the SimulationViewModel instance
            _simulationViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            // Check if the base CanExecute method returns true and the simulation is currently running
            return base.CanExecute(parameter)
                && _simulationViewModel.IsSimulationRunning;
        }

        public override void Execute(object? parameter)
        {
            // Call the StopSimulation method of the SimulationViewModel instance
            _simulationViewModel.StopSimulation();
        }

        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // If the IsSimulationRunning property of the SimulationViewModel instance changes, call the OnExecuteChange method
            if (e.PropertyName == nameof(_simulationViewModel.IsSimulationRunning))
            {
                OnExecuteChange();
            }
        }
    }
}

