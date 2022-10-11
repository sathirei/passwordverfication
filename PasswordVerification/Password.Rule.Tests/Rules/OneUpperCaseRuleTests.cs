using FluentAssertions;
using Xunit;

namespace Password.Rule.Rules.Tests
{
    public class OneUpperCaseRuleests
    {
        [Fact]
        public void Constructor_Should_SetOrderAndMessage()
        {
            // Arrange

            // Act
            var sut = new OneUpperCaseRule(1, "Password should contain at least one upper case.");

            // Assert
            sut.Order.Should().Be(1);
            sut.RuleMessage.Should().Be("Password should contain at least one upper case.");
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        [InlineData("12345678", false)]
        [InlineData("123456789", false)]
        [InlineData("abcdefghi", false)]
        [InlineData("aBcdefghi", true)]
        [InlineData("A", true)]
        public void IsValid_Should_BeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var password = new Core.Password(value);

            // Act
            var sut = new OneUpperCaseRule(1, "Password should contain at least one upper case.");
            var result = sut.IsValid(password);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
