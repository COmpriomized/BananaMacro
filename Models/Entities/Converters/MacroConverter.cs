using BananaMacro.Models.Entities;
using BananaMacro.UI.ViewModels;
using System;

namespace BananaMacro.Models.Converters
{
    public static class MacroConverter
    {
        public static UI.ViewModels.MacroDefinition ToViewModel(this MacroDefinition model)
        {
            if (model == null) return new UI.ViewModels.MacroDefinition();
            return new UI.ViewModels.MacroDefinition(new Models.MacroDefinition
            {
                Id = model.Id,
                Name = model.Name,
                Script = model.Script,
                Enabled = model.Enabled,
                CreatedAt = model.CreatedAt,
                ModifiedAt = model.ModifiedAt,
                Notes = model.Notes
            });
        }

        public static MacroDefinition ToModel(this UI.ViewModels.MacroDefinition vm)
        {
            if (vm == null) return new MacroDefinition();
            var m = vm.Model ?? new MacroDefinition();
            m.Name = vm.Name;
            m.Script = vm.Script;
            m.Enabled = vm.Enabled;
            m.ModifiedAt = vm.ModifiedAt ?? DateTime.UtcNow;
            return m;
        }
    }
}