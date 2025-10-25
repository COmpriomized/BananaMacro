using System.Threading.Tasks;

namespace BananaMacro.DiscordService.Interfaces
{
    public interface IDiscordClient
    {
        Task StartAsync(string token);
        Task StopAsync();
        Task SendMessageAsync(string channelId, string message);
        event Func<string, Task>? OnCommandReceived;
    }
}