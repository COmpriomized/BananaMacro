using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BananaMacro.UI.Helpers;
using BananaMacro.Models.Entities;
using BananaMacro.Extensions;

namespace BananaMacro.UI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<MacroViewModel> Macros { get; } = new ObservableCollection<MacroViewModel>();

        private MacroViewModel? _selectedMacro;
        public MacroViewModel? SelectedMacro
        {
            get => _selectedMacro;
            set => Set(ref _selectedMacro, value);
        }

        public RelayCommand LoadCommand { get; }
        public RelayCommand SaveCommand { get; }

        public MainViewModel()
        {
            LoadCommand = new RelayCommand(async () => await LoadAsync());
            SaveCommand = new RelayCommand(async () => await SaveAsync());
        }

        private async Task LoadAsync()
        {
            var store = ServiceRegistry.Get<Extensions.Interfaces.IStore>();
            if (store == null) return;
            var list = await store.LoadAsync<System.Collections.Generic.List<MacroDefinition>>("macros.json").ConfigureAwait(false);
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                Macros.Clear();
                foreach (var m in list) Macros.Add(new MacroViewModel(m));
            });
        }

        private async Task SaveAsync()
        {
            var store = ServiceRegistry.Get<Extensions.Interfaces.IStore>();
            if (store == null) return;
            var list = new System.Collections.Generic.List<MacroDefinition>();
            foreach (var vm in Macros) list.Add(vm.ToModel());
            await store.SaveAsync("macros.json", list).ConfigureAwait(false);
        }
    }
}