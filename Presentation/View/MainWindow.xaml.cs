using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = AbstractViewModelAPI.CreateViewModelAPI(300, 150);
        }
    }
}
