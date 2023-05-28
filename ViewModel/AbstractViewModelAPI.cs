using Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class AbstractViewModelAPI : INotifyPropertyChanged
    {
        public abstract ICommand StartSimInput { get; }
        public abstract ICommand StopSimInput { get; }
        public abstract ICommand SpawnBallInput { get; }
        public abstract ObservableCollection<object> _balls { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public abstract void SpawnBall();
        public abstract ObservableCollection<object> GetBalls();

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static AbstractViewModelAPI CreateInstance(int windowHeight, int windowWidth)
        {
            return new ViewModel(windowHeight, windowWidth);
        }
    }
}