using System.Threading.Tasks;
using BananaMacro.DiscordService.Core;
using BananaMacro.DiscordService.Models;
using BananaMacro.Extensions.Core;

namespace BananaMacro.DiscordService.Scripts
{
    public static class BotStartup
    {
        public static async Task InitializeAsync()
        {
            var config = new DiscordConfig
            {
                BotToken = "YOUR_BOT_TOKEN",
                DefaultChannelId = "YOUR_CHANNEL_ID",
                ForwardLogs = true
            };

            var client = new DiscordClient();
            ServiceRegistry.Register(client);
            await client.StartAsync(config.BotToken);

            var router = new DiscordCommandRouter(ServiceRegistry.Get<Extensions.Macro.IMacroEngine>()!);
            client.OnCommandReceived += async raw =>
            {
                var cmd = Parse(raw, config);
                await router.HandleAsync(cmd.Command);
            };
        }

        private static DiscordCommand Parse(string raw, DiscordConfig config)
        {
            var parts = raw.Trim().Split(' ');
            return new DiscordCommand
            {
                Raw = raw,
                Command = parts.FirstOrDefault()?.Replace(config.CommandPrefix, "") ?? "",
                Arguments = parts.Skip(1).ToArray(),
                ChannelId = config.DefaultChannelId,
                SenderId = "SimulatedUser"
            };
        }
    }
}