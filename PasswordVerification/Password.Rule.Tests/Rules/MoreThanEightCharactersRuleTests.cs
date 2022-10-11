using FluentAssertions;
using Xunit;

namespace Password.Rule.Rules.Tests
{
    public class MoreThanEightCharactersRuleTests
    {
        [Fact]
        public void Constructor_Should_SetOrderAndMessageAndRuleId()
        {
            // Arrange

            // Act
            var sut = new MoreThanEightCharactersRule(1, 0, "Password should be more than 8 characters in length.");

            // Assert
            sut.Order.Should().Be(0);
            sut.RuleId.Should().Be(1);
            sut.RuleMessage.Should().Be("Password should be more than 8 characters in length.");
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        [InlineData("12345678", false)]
        [InlineData("123456789", true)]
        [InlineData("abcdefghi", true)]
        public void IsValid_Should_BeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var password = new Core.Password(value);

            // Act
            var sut = new MoreThanEightCharactersRule(1, 0, "Password should be more than 8 characters in length.");
            var result = sut.IsValid(password);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
