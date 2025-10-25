using System.Collections.ObjectModel;

namespace BananaMacro.UI.ViewModels
{
    public class LogsViewModel
    {
        public ObservableCollection<string> Lines { get; } = new ObservableCollection<string>();
    }
}