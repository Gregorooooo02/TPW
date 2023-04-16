using System.Windows.Input;

namespace ViewModel
{
    // This abstract class represents a command that can be executed by a UI element in the ViewModel layer.
    // It implements the ICommand interface, which provides methods for querying whether the command can be executed
    // and for executing the command.
    public abstract class Command : ICommand
    {
        // This event is raised when the ability to execute the command changes.
        public event EventHandler? CanExecuteChanged;

        // This method determines whether the command can be executed with the given parameter.
        // By default, all commands can be executed, but derived classes can override this method to provide more specific logic.
        public virtual bool CanExecute(object? parameter) => true;

        // This method executes the command with the given parameter.
        // Derived classes must override this method to provide the specific behavior of the command.
        public abstract void Execute(object? parameter);

        // This method raises the CanExecuteChanged event to notify the UI element that the ability to execute the command has changed.
        // It is called by derived classes when the state of the command changes in a way that affects its ability to be executed.
        protected void OnExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
