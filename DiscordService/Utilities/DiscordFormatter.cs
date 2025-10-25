using BananaMacro.Models.Entities;

namespace BananaMacro.DiscordService.Utilities
{
    public static class DiscordFormatter
    {
        public static string FormatLog(LogEntry entry)
        {
            return $"`[{entry.Timestamp:HH:mm:ss}]` **{entry.Level}**: {entry.Message}";
        }

        public static string FormatMacroStatus(string macroName, bool isRunning)
        {
            return isRunning
                ? $"✅ Macro **{macroName}** is running."
                : $"🛑 Macro **{macroName}** is stopped.";
        }

        public static string FormatCommandHelp()
        {
            return "**Available Commands:**\n" +
                   "`!macro start` — Start default macro\n" +
                   "`!macro stop` — Stop macro\n" +
                   "`!status` — Show active macros\n" +
                   "`!help` — Show this help";
        }
    }
}