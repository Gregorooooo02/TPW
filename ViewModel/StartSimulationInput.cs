using System.ComponentModel;

namespace ViewModel
{
    public class StartSimulationInput : Command
    {
        private SimulationViewModel _simulationViewModel;

        // Constructor that takes a SimulationViewModel instance as a parameter.
        public StartSimulationInput(SimulationViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;
            _simulationViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        // Implementation of the CanExecute method of the ICommand interface.
        // Checks if the command can be executed.
        // Returns true if the base implementation of CanExecute returns true and the simulation is not already running.
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_simulationViewModel.IsSimulationRunning;
        }

        // Implementation of the Execute method of the ICommand interface.
        // Executes the command.
        // Calls the StartSimulation method of the SimulationViewModel instance passed to the constructor.
        public override void Execute(object? parameter)
        {
            _simulationViewModel.StartSimulation();
        }

        // Event handler for the PropertyChanged event of the SimulationViewModel instance.
        // Checks if the IsSimulationRunning property changed.
        // Calls the OnExecuteChange method to indicate that the CanExecute method needs to be reevaluated.
        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simulationViewModel.IsSimulationRunning))
            {
                OnExecuteChange();
            }
        }
    }
}
