using FluentAssertions;
using Xunit;

namespace Password.Rule.Rules.Tests
{
    public class OneLowerCaseRuleests
    {
        [Fact]
        public void Constructor_Should_SetOrderAndMessageAndRuleId()
        {
            // Arrange

            // Act
            var sut = new OneLowerCaseRule(1, 0, "Password should contain at least one lower case.");

            // Assert
            sut.Order.Should().Be(0);
            sut.RuleId.Should().Be(1);
            sut.RuleMessage.Should().Be("Password should contain at least one lower case.");
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("ABC", false)]
        [InlineData("12345678", false)]
        [InlineData("123456789", false)]
        [InlineData("ABCDEFGH", false)]
        [InlineData("ABCDEFGHI", false)]
        [InlineData("Aa", true)]
        [InlineData("abce", true)]
        public void IsValid_Should_BeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var password = new Core.Password(value);

            // Act
            var sut = new OneLowerCaseRule(1, 0, "Password should contain at least one lower case.");
            var result = sut.IsValid(password);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
