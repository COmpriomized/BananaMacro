using Xunit;
using FluentAssertions;
using BananaMacro.Models.Entities;
using BananaMacro.Models.Converters;
using BananaMacro.UI.ViewModels;

namespace BananaMacro.Tests.Unit.UnitTests
{
    public class MacroConverterTests
    {
        [Fact]
        public void ModelToViewModel_AndBack_PreservesCoreFields()
        {
            var model = new MacroDefinition { Name = "M", Script = "x", Enabled = true };
            var vm = model.ToViewModel();
            vm.Should().NotBeNull();
            var round = vm.ToModel();
            round.Name.Should().Be("M");
            round.Script.Should().Be("x");
            round.Enabled.Should().BeTrue();
        }
    }
}