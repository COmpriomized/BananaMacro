using System.Threading.Tasks;

namespace BananaMacro.Integrations.Interfaces
{
    public interface IIntegrationAuthenticator
    {
        Task<bool> ValidateAsync(string credentials);
    }
}