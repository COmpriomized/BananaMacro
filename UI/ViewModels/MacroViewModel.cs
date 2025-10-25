using System;
using System.Windows.Input;
using BananaMacro.UI.Helpers;
using BananaMacro.Models.Entities;
using BananaMacro.Extensions.Macro;

namespace BananaMacro.UI.ViewModels
{
    public class MacroViewModel : ObservableObject
    {
        public MacroDefinition Model { get; }

        public MacroViewModel() : this(new MacroDefinition()) { }

        public MacroViewModel(MacroDefinition model)
        {
            Model = model;
            _name = model.Name;
            _script = model.Script;
            _enabled = model.Enabled;
            StartCommand = new RelayCommand(Start, () => Enabled);
            StopCommand = new RelayCommand(Stop);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (Set(ref _name, value))
                {
                    Model.Name = value;
                    Model.ModifiedAt = DateTime.UtcNow;
                }
            }
        }

        private string _script;
        public string Script
        {
            get => _script;
            set
            {
                if (Set(ref _script, value))
                {
                    Model.Script = value;
                    Model.ModifiedAt = DateTime.UtcNow;
                }
            }
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (Set(ref _enabled, value))
                {
                    Model.Enabled = value;
                    (StartCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private void Start()
        {
            var engine = ServiceRegistry.Get<MacroEngine>();
            if (engine != null) _ = engine.StartAsync(Model);
        }

        private void Stop()
        {
            var engine = ServiceRegistry.Get<MacroEngine>();
            if (engine != null) _ = engine.StopAsync(Model);
        }

        public MacroDefinition ToModel()
        {
            Model.Name = Name;
            Model.Script = Script;
            Model.Enabled = Enabled;
            Model.ModifiedAt = Model.ModifiedAt ?? DateTime.UtcNow;
            return Model;
        }
    }
}