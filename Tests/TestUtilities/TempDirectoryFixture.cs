using System;
using System.IO;

namespace BananaMacro.Tests.Utilities
{
    public class TempDirectoryFixture : IDisposable
    {
        public string Path { get; }

        public TempDirectoryFixture()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Path);
        }

        public void Dispose()
        {
            try { Directory.Delete(Path, true); } catch { }
        }
    }
}