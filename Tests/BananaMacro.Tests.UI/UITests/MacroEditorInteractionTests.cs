using System;
using System.IO;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using Xunit;
using FluentAssertions;
using BananaMacro.Tests.UI.UIAutomation;

namespace BananaMacro.Tests.UI.UITests
{
    public class MacroEditorInteractionTests : IDisposable
    {
        private readonly FlaUIBootstrap _bootstrap;
        private readonly string _outputFolder;

        public MacroEditorInteractionTests()
        {
            _bootstrap = new FlaUIBootstrap();
            _outputFolder = Path.Combine(Path.GetTempPath(), "BananaMacro_UiTests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_outputFolder);

            var exe = ResolveExecutablePath();
            _bootstrap.Launch(exe);
        }

        [Fact]
        public void CanCreateEditAndSaveMacro_viaUI()
        {
            var main = _bootstrap.MainWindow;
            main.Should().NotBeNull();

            // Wait for UI idle
            Retry.WhileFalse(() => main.IsVisible, TimeSpan.FromSeconds(5));

            // Click "Add" in MacrosView toolbar or panel
            var addButton = FindButtonByText(main, "Add");
            addButton.Should().NotBeNull();
            addButton.Invoke();

            // Select the newly created macro in list (first item)
            var listBox = main.FindFirstDescendant(cf => cf.ByControlType(ControlType.List))?.AsListBox();
            listBox.Should().NotBeNull();
            var first = RetryResult.Create(() => listBox.Items.FirstOrDefault(), item => item != null, TimeSpan.FromSeconds(2)).Result;
            first.Should().NotBeNull();
            first.Select();

            // Ensure the editor shows the Name textbox and Script textbox
            var nameBox = main.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("Name")))?.AsTextBox();
            if (nameBox == null)
            {
                // try fallback by searching for a text box near a "Name" label
                var nameLabel = main.FindFirstDescendant(cf => cf.ByText("Name"));
                nameBox = nameLabel?.Parent?.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit))?.AsTextBox();
            }
            nameBox.Should().NotBeNull();
            var scriptBox = main.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("Script")))?.AsTextBox();
            if (scriptBox == null)
            {
                // fallback: pick the large multiline text box
                scriptBox = main.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit))
                                .Select(e => e.AsTextBox())
                                .FirstOrDefault(tb => tb.IsMultiLine);
            }
            scriptBox.Should().NotBeNull();

            // Edit name and script
            var newName = "UI Test Macro " + Guid.NewGuid().ToString("N").Substring(0, 6);
            nameBox.Focus();
            nameBox.Text = newName;

            var newScript = "// UI test script\nprint('hello')";
            scriptBox.Focus();
            scriptBox.Text = newScript;

            // Click Save button in toolbar if present
            var saveButton = FindButtonByText(main, "Save");
            if (saveButton != null)
            {
                saveButton.Invoke();
            }
            else
            {
                // fallback: press Ctrl+S
                Keyboard.Press(VirtualKeyShort.CONTROL);
                Keyboard.Press(VirtualKeyShort.KEY_S);
                Keyboard.Release(VirtualKeyShort.KEY_S);
                Keyboard.Release(VirtualKeyShort.CONTROL);
            }

            // Give app a moment to persist
            System.Threading.Thread.Sleep(500);

            // Close and relaunch to ensure saved state persists in app (optional)
            _bootstrap.Close();
            var exe = ResolveExecutablePath();
            _bootstrap.Launch(exe);

            var main2 = _bootstrap.MainWindow;
            main2.Should().NotBeNull();

            // Find list and the item with name we created
            var listBox2 = main2.FindFirstDescendant(cf => cf.ByControlType(ControlType.List))?.AsListBox();
            listBox2.Should().NotBeNull();

            var matching = RetryResult.Create(() => listBox2.Items.FirstOrDefault(i => string.Equals(i.Text, newName, StringComparison.OrdinalIgnoreCase)),
                                              item => item != null, TimeSpan.FromSeconds(3)).Result;
            matching.Should().NotBeNull();

            // Select and verify script text is present in editor
            matching.Select();
            var scriptBox2 = main2.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit))
                                  .Select(e => e.AsTextBox())
                                  .FirstOrDefault(tb => tb.IsMultiLine);
            scriptBox2.Should().NotBeNull();
            Retry.WhileFalse(() => scriptBox2.Text?.Contains("print('hello')") == true, TimeSpan.FromSeconds(2))
                 .Result.Should().NotBeNull();
        }

        private Button? FindButtonByText(Window main, string text)
        {
            return main.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByText(text)))?.AsButton();
        }

        private string ResolveExecutablePath()
        {
            // Try common output locations relative to test project
            var candidates = new[]
            {
                // typical local debug output
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "src", "BananaMacro.UI", "bin", "Debug", "net6.0-windows", "BananaMacro.UI.exe"),
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "BananaMacro.UI", "bin", "Debug", "net6.0-windows", "BananaMacro.UI.exe"),
                // direct exe in repo root build
                Path.Combine(AppContext.BaseDirectory, "BananaMacro.UI.exe")
            };

            foreach (var c in candidates)
            {
                var full = Path.GetFullPath(c);
                if (File.Exists(full)) return full;
            }

            // If none found, throw a descriptive error so CI can be configured
            throw new FileNotFoundException("Could not find BananaMacro UI executable. Update ResolveExecutablePath with your build output location.");
        }

        public void Dispose()
        {
            try { _bootstrap.Dispose(); } catch { }
            try { Directory.Delete(_outputFolder, true); } catch { }
        }
    }
}