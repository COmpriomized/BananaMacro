using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.Forums
{
    public class ForumSync
    {
        private readonly HttpClient _http;
        private readonly ForumService _service;

        public ForumSync(ForumService service)
        {
            _http = new HttpClient();
            _service = service;
        }

        public async Task SyncFromRemoteAsync(string apiUrl)
        {
            try
            {
                var json = await _http.GetStringAsync(apiUrl).ConfigureAwait(false);
                var remoteThreads = JsonSerializer.Deserialize<List<ForumThread>>(json);
                if (remoteThreads == null) return;

                foreach (var thread in remoteThreads)
                {
                    var existing = _service.GetThreadById(thread.Id);
                    if (existing == null)
                        _service.CreateThread(thread.Title, thread.Author, thread.Tags);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Forum sync failed: {ex.Message}");
            }
        }

        public async Task PushToRemoteAsync(string apiUrl)
        {
            try
            {
                var threads = _service.GetAllThreads();
                var json = JsonSerializer.Serialize(threads, new JsonSerializerOptions { WriteIndented = true });
                var content = new StringContent(json);
                await _http.PostAsync(apiUrl, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Forum push failed: {ex.Message}");
            }
        }
    }
}