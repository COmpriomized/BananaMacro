using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BananaMacro.AI_MachineLearning.Interop
{
    public class JsAiBridge
    {
        private readonly string _scriptPath;

        public JsAiBridge(string scriptPath)
        {
            _scriptPath = scriptPath ?? throw new ArgumentNullException(nameof(scriptPath));
            if (!File.Exists(_scriptPath))
                throw new FileNotFoundException("JavaScript AI module not found", _scriptPath);
        }

        public async Task<AiResult> PredictAsync(string inputText)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = $"\"{_scriptPath}\" \"{EscapeArg(inputText)}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null) throw new InvalidOperationException("Failed to start Node.js process");

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"JavaScript error: {error}");

            return JsonSerializer.Deserialize<AiResult>(output) ?? throw new Exception("Invalid AI result");
        }

        private static string EscapeArg(string arg) => arg.Replace("\"", "\\\"");

        public class AiResult
        {
            public string Label { get; set; } = "";
            public double Confidence { get; set; }
            public object? Raw { get; set; }
        }
    }
}