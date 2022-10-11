using FluentAssertions;
using Xunit;

namespace Password.Rule.Rules.Tests
{
    public class NotNullRuleTests
    {
        [Fact]
        public void Constructor_Should_SetOrderAndMessage()
        {
            // Arrange

            // Act
            var sut = new NotNullRule(1, "Password should be provided.");

            // Assert
            sut.Order.Should().Be(1);
            sut.RuleMessage.Should().Be("Password should be provided.");
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", true)]
        [InlineData("abc", true)]
        [InlineData("12345678", true)]
        [InlineData("123456789", true)]
        [InlineData("abcdefghi", true)]
        public void IsValid_Should_BeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var password = new Core.Password(value);

            // Act
            var sut = new NotNullRule(1, "Password should be provided.");
            var result = sut.IsValid(password);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
