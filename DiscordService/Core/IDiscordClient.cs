using System;
using System.Threading.Tasks;
using BananaMacro.DiscordService.Interfaces;
using BananaMacro.DiscordService.Models;

namespace BananaMacro.DiscordService.Core
{
    public class DiscordClient : IDiscordClient
    {
        private DiscordConfig _config = new();
        public event Func<string, Task>? OnCommandReceived;

        public Task StartAsync(string token)
        {
            _config.BotToken = token;
            // TODO: Connect to Discord using a library like DSharpPlus or Discord.Net
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            // TODO: Disconnect from Discord
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(string channelId, string message)
        {
            // TODO: Send message to Discord channel
            Console.WriteLine($"[Discord] #{channelId}: {message}");
            return Task.CompletedTask;
        }

        // Simulated command listener
        public async Task SimulateCommand(string raw)
        {
            if (OnCommandReceived != null)
                await OnCommandReceived.Invoke(raw);
        }
    }
}