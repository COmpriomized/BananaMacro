using System;
using System.Collections.Concurrent;

namespace BananaMacro.DiscordService.Utilities
{
    public class DiscordRateLimiter
    {
        private readonly ConcurrentDictionary<string, DateTime> _lastSent = new();
        private readonly TimeSpan _cooldown = TimeSpan.FromSeconds(3);

        public bool CanSend(string channelId)
        {
            var now = DateTime.UtcNow;
            if (_lastSent.TryGetValue(channelId, out var last))
            {
                if ((now - last) < _cooldown) return false;
            }

            _lastSent[channelId] = now;
            return true;
        }
    }
}