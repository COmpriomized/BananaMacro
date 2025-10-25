namespace BananaMacro.DiscordService.Models
{
    public class DiscordCommand
    {
        public string Raw { get; set; } = string.Empty;
        public string Command { get; set; } = string.Empty;
        public string[] Arguments { get; set; } = Array.Empty<string>();
        public string SenderId { get; set; } = string.Empty;
        public string ChannelId { get; set; } = string.Empty;
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

        public override string ToString() => $"{Command} ({Arguments.Length} args) from {SenderId}";
    }
}