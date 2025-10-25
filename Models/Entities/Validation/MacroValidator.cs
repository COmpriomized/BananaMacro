using System;
using BananaMacro.Models.Entities;

namespace BananaMacro.Models.Validation
{
    public static class MacroValidator
    {
        public static bool IsValid(MacroDefinition macro, out string reason)
        {
            if (macro == null)
            {
                reason = "Macro is null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(macro.Name))
            {
                reason = "Name is required";
                return false;
            }

            if (macro.Script == null)
            {
                reason = "Script cannot be null";
                return false;
            }

            if (macro.Script.Length > 1_000_000)
            {
                reason = "Script is too long";
                return false;
            }

            reason = string.Empty;
            return true;
        }
    }
}