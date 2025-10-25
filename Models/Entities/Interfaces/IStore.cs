using System.Collections.Generic;
using System.Threading.Tasks;

namespace BananaMacro.Models.Interfaces
{
    public interface IStore
    {
        Task<T> LoadAsync<T>(string relativePath) where T : new();
        Task SaveAsync<T>(string relativePath, T data);
        Task<IEnumerable<string>> ListFilesAsync(string relativeFolder);
    }
}