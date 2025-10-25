using System;
using Xunit;
using FluentAssertions;
using BananaMacro.Models.Entities;

namespace BananaMacro.Tests.Unit.UnitTests
{
    public class MacroDefinitionTests
    {
        [Fact]
        public void NewMacro_HasNonEmptyIdAndCreatedAt()
        {
            var m = new MacroDefinition();
            m.Id.Should().NotBeNullOrWhiteSpace();
            m.CreatedAt.Should().BeOnOrBefore(DateTime.UtcNow);
        }

        [Fact]
        public void SettingName_UpdatesModel()
        {
            var m = new MacroDefinition { Name = "old" };
            m.Name = "new";
            m.Name.Should().Be("new");
        }
    }
}