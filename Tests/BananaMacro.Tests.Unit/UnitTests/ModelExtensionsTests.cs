using System;
using Xunit;
using FluentAssertions;
using BananaMacro.Models.Entities;
using BananaMacro.Models.Utilities;

namespace BananaMacro.Tests.Unit.UnitTests
{
    public class ModelExtensionsTests
    {
        [Fact]
        public void TouchModified_SetsModifiedAt()
        {
            var m = new MacroDefinition();
            m.ModifiedAt.Should().BeNull();
            m.TouchModified();
            m.ModifiedAt.Should().NotBeNull();
            m.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
        }

        [Fact]
        public void ShortInfo_IncludesNameAndStatus()
        {
            var m = new MacroDefinition { Name = "T", Enabled = false };
            var info = m.ShortInfo();
            info.Should().Contain("T");
            info.Should().Contain("Disabled");
        }
    }
}