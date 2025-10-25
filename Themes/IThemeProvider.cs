using System.Collections.Generic;
using System.Threading.Tasks;

namespace BananaMacro.Themes
{
    public interface IThemeProvider
    {
        Task<IEnumerable<ThemeModel>> GetAvailableAsync();
        Task<ThemeModel?> GetByIdAsync(string id);
        Task SaveAsync(ThemeModel theme);
        Task DeleteAsync(string id);
    }
}