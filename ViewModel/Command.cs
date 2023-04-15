using System.Windows.Input;

namespace ViewModel
{
    public abstract class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => true;
        public abstract void Execute(object? parameter);
        protected void OnExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
