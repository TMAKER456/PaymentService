using Smartwyre.DeveloperTest.Runner;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void ProgramNullArgs_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Program.Main(null));
        }

        [Fact]
        public void ProgramValidArgs_ReturnsErrorCode()
        {
            // No accounts to read
            Assert.Equal(1, Program.Main(new[] { "abc", "12.4" }));
        }
    }
}
