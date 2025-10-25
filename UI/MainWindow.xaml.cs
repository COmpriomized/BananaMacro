using System.Windows;
using BananaMacro.UI.ViewModels;

namespace BananaMacro.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}