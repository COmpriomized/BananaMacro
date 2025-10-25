using System.Threading.Tasks;
using BananaMacro.DiscordService.Models;
using BananaMacro.Extensions.Macro;
using BananaMacro.Models.Entities;

namespace BananaMacro.DiscordService.Core
{
    public class DiscordCommandRouter
    {
        private readonly IMacroEngine _engine;

        public DiscordCommandRouter(IMacroEngine engine)
        {
            _engine = engine;
        }

        public async Task HandleAsync(string command)
        {
            if (command.StartsWith("!macro start"))
            {
                var macro = new MacroDefinition { Name = "DiscordMacro", Script = "print('Hello from Discord')" };
                await _engine.StartAsync(macro);
            }
            else if (command.StartsWith("!status"))
            {
                var active = _engine.GetActiveMacroIds();
                Console.WriteLine($"Active macros: {string.Join(", ", active)}");
            }
        }
    }
}