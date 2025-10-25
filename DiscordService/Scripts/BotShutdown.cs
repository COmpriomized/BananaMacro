using System.Threading.Tasks;
using BananaMacro.DiscordService.Interfaces;
using BananaMacro.Extensions.Core;

namespace BananaMacro.DiscordService.Scripts
{
    public static class BotShutdown
    {
        public static async Task ShutdownAsync()
        {
            var client = ServiceRegistry.Get<IDiscordClient>();
            if (client != null)
            {
                await client.StopAsync();
            }
        }
    }
}