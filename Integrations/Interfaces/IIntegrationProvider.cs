using System.Collections.Generic;

namespace BananaMacro.Integrations.Interfaces
{
    public interface IIntegrationProvider
    {
        IEnumerable<string> GetAvailableIntegrationIds();
        IIntegration? Create(string integrationId);
    }
}