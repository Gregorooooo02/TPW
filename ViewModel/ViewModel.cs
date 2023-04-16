using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    // This abstract class represents a ViewModel, which is responsible for providing data and behavior for a View in the MVVM framework.
    // It implements the INotifyPropertyChanged interface, which allows UI elements to listen for changes to the ViewModel's properties.
    public abstract class ViewModel : INotifyPropertyChanged
    {
        // This event is raised when a property of the ViewModel changes.
        public event PropertyChangedEventHandler? PropertyChanged;

        // This method raises the PropertyChanged event to notify the UI that a property has changed.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This method sets the value of a field and raises the PropertyChanged event if the value has changed.
        // The propertyName parameter is automatically filled with the name of the calling property, so it does not need to be passed in explicitly.
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                // If the new value is the same as the current value, there is no need to update the field or raise the event.
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);

            // Return true to indicate that the value has changed.
            return true;
        }
    }
}
