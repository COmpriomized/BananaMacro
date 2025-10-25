using Xunit;
using FluentAssertions;
using BananaMacro.UI.ViewModels;

namespace BananaMacro.Tests.UI.UITests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void MainViewModel_InitializesWithEmptyCollection()
        {
            var vm = new MainViewModel();
            vm.Macros.Should().NotBeNull();
            vm.Macros.Should().BeEmpty();
        }
    }
}