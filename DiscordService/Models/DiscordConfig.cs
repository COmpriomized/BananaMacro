namespace BananaMacro.DiscordService.Models
{
    public class DiscordConfig
    {
        public string BotToken { get; set; } = string.Empty;
        public string DefaultChannelId { get; set; } = string.Empty;
        public bool ForwardLogs { get; set; } = true;
        public string CommandPrefix { get; set; } = "!";
    }
}