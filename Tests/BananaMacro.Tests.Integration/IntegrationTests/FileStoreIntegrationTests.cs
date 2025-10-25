using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BananaMacro.Extensions.Stores;
using BananaMacro.Models.Entities;

namespace BananaMacro.Tests.Integration.IntegrationTests
{
    public class FileStoreIntegrationTests : IDisposable
    {
        private readonly string _tempDir;

        public FileStoreIntegrationTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDir);
        }

        [Fact]
        public async Task SaveAndLoad_ListOfMacros_Persists()
        {
            var store = new FileStore(_tempDir);
            var macros = new[] { new MacroDefinition { Name = "t1" }, new MacroDefinition { Name = "t2" } };
            await store.SaveAsync("macros.json", macros);
            var loaded = await store.LoadAsync<MacroDefinition[]>("macros.json");
            loaded.Should().HaveCount(2);
            loaded[0].Name.Should().Be("t1");
        }

        public void Dispose()
        {
            try { Directory.Delete(_tempDir, true); } catch { }
        }
    }
}