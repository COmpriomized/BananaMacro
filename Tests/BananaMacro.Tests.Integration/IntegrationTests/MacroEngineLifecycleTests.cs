using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BananaMacro.Extensions.Macro;
using BananaMacro.Extensions.Logging;
using BananaMacro.Extensions.Stores;
using BananaMacro.Models.Entities;

namespace BananaMacro.Tests.Integration.IntegrationTests
{
    public class MacroEngineLifecycleTests
    {
        [Fact]
        public async Task StartAndStop_MacroRunner_LifecycleWorks()
        {
            var tmp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.Guid.NewGuid().ToString("N"));
            var store = new FileStore(tmp);
            var logger = new LoggingService(store);
            var engine = new MacroEngine(logger);

            var macro = new MacroDefinition { Name = "t", Script = "line1\nline2", Enabled = true };
            await engine.StartAsync(macro);
            // allow the runner to start and emit a couple of logs
            await Task.Delay(300);
            await engine.StopAsync(macro);

            engine.GetActiveMacroIds().Should().NotContain(macro.Id);

            logger.Dispose();
            try { System.IO.Directory.Delete(tmp, true); } catch { }
        }
    }
}