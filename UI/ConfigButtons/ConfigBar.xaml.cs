using System.Windows;
using System.Windows.Controls;
using BananaMacro.UI.UIHelpers;
using BananaMacro.UI.ViewModels;
using System.IO;

namespace BananaMacro.UI.ConfigButtons
{
    public partial class ConfigBar : UserControl
    {
        public ConfigBar()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine("SavedData", "config.json");
            // safe-read into MainViewModel settings container
            var vm = (Application.Current.MainWindow.DataContext as MainViewModel);
            if (vm != null)
            {
                vm.LoadConfig(path);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine("SavedData", "config.json");
            var vm = (Application.Current.MainWindow.DataContext as MainViewModel);
            if (vm != null)
            {
                vm.SaveConfig(path);
            }
        }
    }
}