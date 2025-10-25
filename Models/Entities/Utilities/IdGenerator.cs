using System;

namespace BananaMacro.Models.Utilities
{
    public static class IdGenerator
    {
        public static string NewId() => Guid.NewGuid().ToString("D");
    }
}