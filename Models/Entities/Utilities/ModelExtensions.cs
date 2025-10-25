using System;
using BananaMacro.Models.Entities;

namespace BananaMacro.Models.Utilities
{
    public static class ModelExtensions
    {
        public static void TouchModified(this MacroDefinition macro)
        {
            macro.ModifiedAt = Timestamps.UtcNow;
        }

        public static string ShortInfo(this MacroDefinition macro)
        {
            return $"{macro.Name} ({(macro.Enabled ? "Enabled" : "Disabled")})";
        }
    }
}