using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class AbstractViewModelAPI : INotifyPropertyChanged
    {
        public abstract ObservableCollection<object> Balls { get; set; }
        public abstract ICommand StartCommand { get; }
        public abstract ICommand StopCommand { get; }
        public abstract ICommand CreateBallCommand { get; }
        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<object> GetBalls();


        public static AbstractViewModelAPI CreateViewModelAPI(int boardWidth, int boardHeight)
        {
            return new ViewModel(boardWidth, boardHeight);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
