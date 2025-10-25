using System;
using System.Windows.Input;
using BananaMacro.Models;
using BananaMacro.UI.UIHelpers;

namespace BananaMacro.UI.ViewModels
{
    public class MacroDefinition : ObservableObject
    {
        public MacroDefinition() : this(new Models.MacroDefinition()) { }

        public MacroDefinition(Models.MacroDefinition model)
        {
            Model = model ?? new Models.MacroDefinition();
            _name = Model.Name;
            _script = Model.Script;
            _enabled = Model.Enabled;
            CreatedAt = Model.CreatedAt;
            ModifiedAt = Model.ModifiedAt;
            StartCommand = new RelayCommand(Start, () => Enabled);
            StopCommand = new RelayCommand(Stop);
        }

        public Models.MacroDefinition Model { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (Set(ref _name, value))
                {
                    Model.Name = value;
                    ModifiedAt = DateTime.UtcNow;
                    Raise(nameof(Name));
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
                    ModifiedAt = DateTime.UtcNow;
                    Raise(nameof(Script));
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
                    Raise(nameof(Enabled));
                    (StartCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime CreatedAt { get; }
        private DateTime? _modifiedAt;
        public DateTime? ModifiedAt
        {
            get => _modifiedAt;
            private set => Set(ref _modifiedAt, value);
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private void Start()
        {
            // Hook into MacroEngine to start this macro.
            // Placeholder: update state if needed.
        }

        private void Stop()
        {
            // Hook into MacroEngine to stop this macro.
            // Placeholder: update state if needed.
        }

        public Models.MacroDefinition ToModel()
        {
            Model.Name = Name;
            Model.Script = Script;
            Model.Enabled = Enabled;
            Model.ModifiedAt = ModifiedAt ?? DateTime.UtcNow;
            return Model;
        }
    }
}