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
            Func<string, bool> valueEquals = s => s == value;
            var isValueSet = passwd.Evaluate(valueEquals);

            // Assert
            isValueSet.Should().BeTrue();
        }
    }
}