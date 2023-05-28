using ViewModel;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() : base()
        {
            InitializeComponent();
            DataContext = AbstractViewModelAPI.CreateInstance(150, 300);
        }
    }
}
