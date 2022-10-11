using Xunit;
using FluentAssertions;

namespace Password.RuleSet.Tests
{
    public class AtLeastNRuleSetTests
    {
        [Fact()]
        public void IsValid_Should_ReturnTrue_WhenValidRuleResultCountIsEqualToCount()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, false }, { 4, true } };

            // Act
            var sut = new AtLeastNRuleSet(ruleSetId: 1, ruleCount: 3);
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeTrue();
        }

        [Fact()]
        public void IsValid_Should_ReturnTrue_WhenRuleResultCountIsGreaterThanCount()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, true }, { 4, true } };

            // Act
            var sut = new AtLeastNRuleSet(ruleSetId: 1, ruleCount: 3);
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeTrue();
        }

        [Fact()]
        public void IsValid_Should_ReturnFalse_WhenRuleResultCountIsLessThanCount()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, false }, { 4, true } };

            // Act
            var sut = new AtLeastNRuleSet(ruleSetId: 1, ruleCount: 4);
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeFalse();
        }


        [Fact()]
        public void IsValid_Should_ReturnTrue_WhenAtLeastNRuleIsSetToZeroRules()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, false }, { 2, false }, { 3, false }, { 4, false } };

            // Act
            var sut = new AtLeastNRuleSet(ruleSetId: 1, ruleCount: 0);
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeTrue();
        }
    }
}