using BananaMacro.Extensions.Logging;
using BananaMacro.Extensions.Interfaces;
using BananaMacro.Integrations.Models;

namespace BananaMacro.Integrations.Core
{
    public class IntegrationContext
    {
        public IStore Store { get; }
        public LoggingService Logger { get; }

        public IntegrationContext(IStore store, LoggingService logger)
        {
            Store = store;
            Logger = logger;
        }
    }
}