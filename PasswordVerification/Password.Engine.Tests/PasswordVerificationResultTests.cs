using Xunit;
using FluentAssertions;

namespace Password.Engine.Tests
{
    public class PasswordVerificationResultTests
    {
        [Fact()]
        public void IsVerifiedOkay_Should_BeTrue_WhenAllRuleSetPasses()
        {
            // Arrange
            var ruleResults = new List<Result>
            {
                new Result(1, true, "RuleOne"),
                new Result(2, false, "RuleTwo"),
            };

            var ruleSetResults = new List<Result>
            {
                new Result(1, true, "RuleSetOne"),
                new Result(2, true, "RuleSetTwo"),
            };

            // Act
            var sut = new PasswordVerificationResult(ruleResults, ruleSetResults);

            // Assert
            sut.IsVerifiedOkay.Should().BeTrue();
        }

        [Fact()]
        public void IsVerifiedOkay_Should_BeFalse_WhenAtLeastOneRuleSetFailed()
        {
            // Arrange
            var ruleResults = new List<Result>
            {
                new Result(1, true, "RuleOne"),
                new Result(2, false, "RuleTwo"),
            };

            var ruleSetResults = new List<Result>
            {
                new Result(1, true, "RuleSetOne"),
                new Result(2, pass: false, "RuleSetTwo"),
            };

            // Act
            var sut = new PasswordVerificationResult(ruleResults, ruleSetResults);

            // Assert
            sut.IsVerifiedOkay.Should().BeFalse();
        }

        [Fact()]
        public void IsVerifiedOkay_Should_BeFalse_WhenNoRuleSetsButAtLeastOneRuleIsFales()
        {
            // Arrange
            var ruleResults = new List<Result>
            {
                new Result(1, true, "RuleOne"),
                new Result(2, false, "RuleTwo"),
            };

            // Act
            var sut = new PasswordVerificationResult(ruleResults, null);

            // Assert
            sut.IsVerifiedOkay.Should().BeFalse();
        }

        [Fact()]
        public void IsVerifiedOkay_Should_BeTrue_WhenNoRuleOrRuleSetsAreConfigured()
        {
            // Arrange

            // Act
            var sut = new PasswordVerificationResult(null, null);

            // Assert
            sut.IsVerifiedOkay.Should().BeTrue();
        }
    }
}