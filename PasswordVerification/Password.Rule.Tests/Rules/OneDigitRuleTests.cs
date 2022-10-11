using FluentAssertions;
using Xunit;

namespace Password.Rule.Rules.Tests
{
    public class OneDigitRuleests
    {
        [Fact]
        public void Constructor_Should_SetOrderAndMessage()
        {
            // Arrange

            // Act
            var sut = new OneDigitRule(1, "Password should contain at least one digit.");

            // Assert
            sut.Order.Should().Be(1);
            sut.RuleMessage.Should().Be("Password should contain at least one digit.");
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        [InlineData("12345678", true)]
        [InlineData("123456789", true)]
        [InlineData("abcdefghi", false)]
        [InlineData("a9cdefghi", true)]
        [InlineData("A", false)]
        public void IsValid_Should_BeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var password = new Core.Password(value);

            // Act
            var sut = new OneDigitRule(1, "Password should contain at least one digit.");
            var result = sut.IsValid(password);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
