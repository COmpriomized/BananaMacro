using System.Collections.ObjectModel;
using BananaMacro.UI.Helpers;
using BananaMacro.Models.Entities;

namespace BananaMacro.UI.ViewModels
{
    public class MacrosViewModel : ObservableObject
    {
        public ObservableCollection<MacroViewModel> Items { get; } = new ObservableCollection<MacroViewModel>();

        private MacroViewModel? _selected;
        public MacroViewModel? Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public MacrosViewModel()
        {
            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove, () => Selected != null);
        }

        private void Add()
        {
            var model = new MacroDefinition { Name = "New Macro" };
            var vm = new MacroViewModel(model);
            Items.Add(vm);
            Selected = vm;
            RemoveCommand.RaiseCanExecuteChanged();
        }

        private void Remove()
        {
            if (Selected == null) return;
            Items.Remove(Selected);
            Selected = null;
            RemoveCommand.RaiseCanExecuteChanged();
        }
    }
}