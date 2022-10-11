using Xunit;
using FluentAssertions;

namespace Password.RuleSet.Tests
{
    public class MandatoryRuleSetTests
    {
        [Fact()]
        public void IsValid_Should_ReturnTrue_WhenAllMandatoryRulesAreValid()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, false }, { 4, true } };

            // Act
            var sut = new MandatoryRuleSet(ruleSetId: 1, mandatoryRuleIds: new List<int> { 1, 4 });
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeTrue();
        }

        [Fact()]
        public void IsValid_Should_ReturnFalse_WhenSomeMandatoryRulesAreNotValid()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, true }, { 2, true }, { 3, true }, { 4, false } };

            // Act
            var sut = new MandatoryRuleSet(ruleSetId: 1, mandatoryRuleIds: new List<int> { 1, 4 });
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeFalse();
        }

        [Fact()]
        public void IsValid_Should_ReturnFalse_WhenAllMandatoryRulesAreNotValid()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false }, { 4, false } };

            // Act
            var sut = new MandatoryRuleSet(ruleSetId: 1, mandatoryRuleIds: new List<int> { 1, 4 });
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeFalse();
        }

        [Fact()]
        public void IsValid_Should_ReturnTrue_WhenNoMandatoryRulesAreSet()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false }, { 4, false } };

            // Act
            var sut = new MandatoryRuleSet(ruleSetId: 1, mandatoryRuleIds: null);
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeTrue();
        }


        [Fact()]
        public void IsValid_Should_ReturnFalse_WhenAllMandatoryRulesAreNotIntheRuleResult()
        {
            // Arrange
            var ruleResults = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false }, { 4, false } };

            // Act
            var sut = new MandatoryRuleSet(ruleSetId: 1, mandatoryRuleIds: new List<int> { 1, 99 });
            var result = sut.IsValid(ruleResults);

            // Assert
            result.Should().BeFalse();
        }
    }
}