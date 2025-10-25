using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using FlaUI.UIA3;

namespace BananaMacro.Tests.UI.UIAutomation
{
    public class FlaUIBootstrap : IDisposable
    {
        public Application? App { get; private set; }
        public AutomationBase? Automation { get; private set; }
        public Window? MainWindow { get; private set; }
        public string? ExecutablePath { get; private set; }

        public FlaUIBootstrap() { }

        public void Launch(string exePath, string? arguments = null, int launchTimeoutMs = 10_000)
        {
            if (string.IsNullOrWhiteSpace(exePath)) throw new ArgumentException("exePath required", nameof(exePath));
            ExecutablePath = exePath;
            App = Application.Launch(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arguments ?? string.Empty,
                UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(exePath) ?? Environment.CurrentDirectory
            });

            Automation = new UIA3Automation();
            MainWindow = WaitForMainWindow(launchTimeoutMs);
            if (MainWindow == null) throw new InvalidOperationException("Main window did not appear within the timeout");
        }

        public Window? WaitForMainWindow(int timeoutMs = 10_000)
        {
            if (App == null || Automation == null) return null;
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                try
                {
                    var mw = App.GetMainWindow(Automation);
                    if (mw != null && mw.IsVisible) return mw;
                }
                catch { /* ignore transient reflection issues while app starts */ }

                Thread.Sleep(100);
            }
            return null;
        }

        public void AttachToExisting(int processId, int attachTimeoutMs = 5000)
        {
            App?.Close();
            App = Application.Attach(processId);
            Automation = new UIA3Automation();
            MainWindow = WaitForMainWindow(attachTimeoutMs);
            if (MainWindow == null) throw new InvalidOperationException("Failed to attach to main window");
        }

        public void Close()
        {
            try
            {
                MainWindow?.Close();
            }
            catch { }

            try
            {
                if (App != null && !App.HasExited)
                {
                    App.Close();
                    if (!App.WaitWhileMainHandleExists(TimeSpan.FromSeconds(2)))
                    {
                        App.Kill();
                    }
                }
            }
            catch { }

            DisposeAutomation();
        }

        public string CaptureScreenshot(string outputFolder, string fileName = null)
        {
            if (Automation == null) throw new InvalidOperationException("Automation not initialized");
            Directory.CreateDirectory(outputFolder);
            var name = fileName ?? $"screenshot_{DateTime.UtcNow:yyyyMMdd_HHmmss_fff}.png";
            var path = Path.Combine(outputFolder, name);

            try
            {
                using var img = Capture.Screen();
                img.ToFile(path);
                return path;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void DisposeAutomation()
        {
            try { Automation?.Dispose(); } catch { }
            Automation = null;
            MainWindow = null;
            App = null;
        }

        public void Dispose()
        {
            Close();
            DisposeAutomation();
        }
    }
}