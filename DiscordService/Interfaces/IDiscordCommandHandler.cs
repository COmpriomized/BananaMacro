using System.Threading.Tasks;
using BananaMacro.DiscordService.Models;

namespace BananaMacro.DiscordService.Interfaces
{
    public interface IDiscordCommandHandler
    {
        Task<bool> CanHandleAsync(DiscordCommand command);
        Task HandleAsync(DiscordCommand command);
    }
}