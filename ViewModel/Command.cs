using System.Windows.Input;

namespace ViewModel
{
    // This class represents a command that can be executed by a UI element in the ViewModel layer.
    // It implements the ICommand interface, which provides methods for querying whether the command can be executed
    // and for executing the command.
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // This method determines whether the command can be executed with the given parameter.
        // By default, all commands can be executed, but derived classes can override this method to provide more specific logic.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // This method executes the command with the given parameter.
        // Derived classes must override this method to provide the specific behavior of the command.
        public void Execute(object parameter)
        {
            _execute();
        }

        // This method raises the CanExecuteChanged event to notify the UI element that the ability to execute the command has changed.
        // It is called by derived classes when the state of the command changes in a way that affects its ability to be executed.
        protected virtual void OnExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
