using FluentAssertions;
using Xunit;

namespace Password.Core.Tests
{
    public class PasswordTests
    {
        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abcd")]
        public void Constructor_Should_SetValue(string value)
        {
            // Arrange

            // Act
            var passwd = new Password(value);

            // Assert
            passwd.Value.Should().Be(value);
        }
    }
}