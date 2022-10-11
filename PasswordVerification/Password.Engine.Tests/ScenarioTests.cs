using FluentAssertions;
using Password.Rule.Rules;
using Password.RuleSet;
using Xunit;

namespace Password.Engine.Tests
{
    public class ScenarioTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("12345678", false)]
        [InlineData("123456789", true)]
        public void MoreThanEightCharactersRule_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(new MoreThanEightCharactersRule(1, 1, "Password should be more than 8 characters in length."));

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", true)]
        [InlineData("abcdefghi", true)]
        public void NotNullRule_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(new NotNullRule(1, 1, "Password should be provided."));

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        [InlineData("123456789", true)]
        public void OneDigitRule_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(new OneDigitRule(1, 1, "Password should contain at least one digit."));

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("ABC", false)]
        [InlineData("123456789", false)]
        [InlineData("Aa", true)]
        [InlineData("abce", true)]
        public void OneLowerCaseRule_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(new OneLowerCaseRule(1, 1, "Password should contain at least one lower case."));

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc", false)]
        [InlineData("123456789", false)]
        [InlineData("aBcdefghi", true)]
        [InlineData("A", true)]
        public void OneUpperCaseRule_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(new OneUpperCaseRule(1, 1, "Password should contain at least one uppper case."));

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc1", true)]
        [InlineData("123456789", true)]
        [InlineData("aBcdefghi9", true)]
        public void AtLeastNRuleSet_ResultShouldBeAsExpected_WhenNIsSetTo3(string value, bool expectedResult)
        {
            // Arrange
            var moreThanEightCharactersRule = new MoreThanEightCharactersRule(1, 1, "Password should be more than 8 characters in length.");
            var notNullRule = new NotNullRule(2, 2, "Password should be provided.");
            var oneDigitRule = new OneDigitRule(3, 3, "Password should contain at least one digit.");
            var oneLowerCaseRule = new OneLowerCaseRule(4, 4, "Password should contain at least one lower case.");
            var oneUpperCaseRule = new OneUpperCaseRule(5, 5, "Password should contain at least one lower case.");

            var atleastThreeRuleSet = new AtLeastNRuleSet(1, 3);

            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(moreThanEightCharactersRule)
                .AddRule(notNullRule)
                .AddRule(oneDigitRule)
                .AddRule(oneLowerCaseRule)
                .AddRule(oneUpperCaseRule)
                .AddRuleSet(atleastThreeRuleSet);

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("abc1", false)]
        [InlineData("123456789", false)]
        [InlineData("aBcdefghi9", true)]
        public void AtLeastThreeAndOneMandatoryRuleSets_ResultShouldBeAsExpected(string value, bool expectedResult)
        {
            // Arrange
            var moreThanEightCharactersRule = new MoreThanEightCharactersRule(1, 1, "Password should be more than 8 characters in length.");
            var notNullRule = new NotNullRule(2, 2, "Password should be provided.");
            var oneDigitRule = new OneDigitRule(3, 3, "Password should contain at least one digit.");
            var oneLowerCaseRule = new OneLowerCaseRule(4, 4, "Password should contain at least one lower case.");
            var oneUpperCaseRule = new OneUpperCaseRule(5, 5, "Password should contain at least one lower case.");

            var atleastThreeRuleSet = new AtLeastNRuleSet(1, 3);
            var mandatoryRuleSet = new MandatoryRuleSet(2, new List<int> { 1, 2, 3, 4, 5 });

            var ruleEngine = new RuleEngine();
            ruleEngine.AddRule(moreThanEightCharactersRule)
                .AddRule(notNullRule)
                .AddRule(oneDigitRule)
                .AddRule(oneLowerCaseRule)
                .AddRule(oneUpperCaseRule)
                .AddRuleSet(atleastThreeRuleSet)
                .AddRuleSet(mandatoryRuleSet);

            // Act
            var result = ruleEngine.Evaluate(new Core.Password(value));

            // Assert
            result.IsVerifiedOkay.Should().Be(expectedResult);
        }
    }
}
