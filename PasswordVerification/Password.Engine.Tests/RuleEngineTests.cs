using Xunit;
using Moq;
using Password.Core.Rule;
using FluentAssertions;
using Password.Engine.Exceptions;
using Password.RuleSet;

namespace Password.Engine.Tests
{
    public class RuleEngineTests
    {
        [Fact()]
        public void AddRule_Should_Pass()
        {
            // Arrange
            var mockRule = new Mock<IPasswordRule>();
            mockRule.SetupGet(x => x.RuleId).Returns(1).Verifiable();

            // Act
            var sut = new RuleEngine();
            Action act = () => sut.AddRule(mockRule.Object);

            // Assert
            act.Should().NotThrow<AddRuleException>();
            mockRule.VerifyAll();
        }

        [Fact()]
        public void AddRule_Should_ThrowAddRuleException()
        {
            // Arrange
            var mockRule = new Mock<IPasswordRule>();
            mockRule.SetupGet(x => x.RuleId).Returns(1);
            mockRule.SetupGet(x => x.RuleMessage).Returns("Hello");

            // Act
            var sut = new RuleEngine();
            sut.AddRule(mockRule.Object);
            Action act = () => sut.AddRule(mockRule.Object);

            // Assert
            act.Should().Throw<AddRuleException>().WithMessage("Adding to rules failed for rule with ID: 1, Rule: Hello");
            mockRule.VerifyAll();
        }

        [Fact()]
        public void AddRuleSet_Should_Pass()
        {
            // Arrange
            var mockRuleSet = new Mock<IPasswordRuleSet>();
            mockRuleSet.SetupGet(x => x.RuleSetId).Returns(1);

            // Act
            var sut = new RuleEngine();
            Action act = () => sut.AddRuleSet(mockRuleSet.Object);

            // Assert
            act.Should().NotThrow<AddRuleSetException>();
            mockRuleSet.VerifyAll();
        }

        [Fact()]
        public void AddRuleSet_Should_ThrowAddRuleSetException()
        {
            // Arrange
            var mockRuleSet = new Mock<IPasswordRuleSet>();
            mockRuleSet.SetupGet(x => x.RuleSetId).Returns(1);

            // Act
            var sut = new RuleEngine();
            sut.AddRuleSet(mockRuleSet.Object);
            Action act = () => sut.AddRuleSet(mockRuleSet.Object);

            // Assert
            act.Should().Throw<AddRuleSetException>().WithMessage("Adding to rule sets failed for rule set with ID: 1, RuleSet: IPasswordRuleSetProxy");
            mockRuleSet.VerifyAll();
        }

        [Fact()]
        public void Evaluate_Should_VerifyPasswordAsOkay_WhenNoRulesAreConfigured()
        {
            // Arrange
            var password = new Core.Password("hello");
            var sut = new RuleEngine();

            // Act
            var result = sut.Evaluate(password);

            // Assert
            result.IsVerifiedOkay.Should().BeTrue();
            result.RuleResults.Should().BeEmpty();
            result.RuleSetResults.Should().BeEmpty();
        }

        [Fact()]
        public void Evaluate_Should_VerifyPasswordAsNotOkay_WhenRulesAreConfiguredAndOneOfThemFails()
        {
            // Arrange
            var mockRuleOne = new Mock<IPasswordRule>();
            mockRuleOne.SetupGet(x => x.RuleId).Returns(1).Verifiable();
            mockRuleOne.SetupGet(x => x.RuleMessage).Returns("RuleOne").Verifiable();
            var mockRuleTwo = new Mock<IPasswordRule>();
            mockRuleTwo.SetupGet(x => x.RuleId).Returns(2).Verifiable();
            mockRuleTwo.SetupGet(x => x.RuleMessage).Returns("RuleTwo").Verifiable();

            var password = new Core.Password("hello");

            mockRuleOne.Setup(x => x.IsValid(password)).Returns(true).Verifiable();
            mockRuleTwo.Setup(x => x.IsValid(password)).Returns(false).Verifiable();


            var sut = new RuleEngine();
            sut.AddRule(mockRuleOne.Object)
                .AddRule(mockRuleTwo.Object);

            // Act
            var result = sut.Evaluate(password);

            // Assert
            result.IsVerifiedOkay.Should().BeFalse();
            result.RuleResults.Should().HaveCount(2);
            result.RuleSetResults.Should().BeEmpty();
            mockRuleOne.VerifyAll();
            mockRuleTwo.VerifyAll();
        }

        [Fact()]
        public void Evaluate_Should_VerifyPasswordAsNotOkay_WhenRulesAndRuleSetsAreConfiguredAndNotAllRuleSetPasses()
        {
            // Arrange
            var mockRuleOne = new Mock<IPasswordRule>();
            mockRuleOne.SetupGet(x => x.RuleId).Returns(1).Verifiable();
            mockRuleOne.SetupGet(x => x.RuleMessage).Returns("RuleOne").Verifiable();
            var mockRuleTwo = new Mock<IPasswordRule>();
            mockRuleTwo.SetupGet(x => x.RuleId).Returns(2).Verifiable();
            mockRuleTwo.SetupGet(x => x.RuleMessage).Returns("RuleTwo").Verifiable();

            var mockRuleSetOne = new Mock<IPasswordRuleSet>();
            mockRuleSetOne.SetupGet(x => x.RuleSetId).Returns(1).Verifiable();
            mockRuleSetOne.SetupGet(x => x.RuleSetMessage).Returns("RuleSetOne").Verifiable();
            var mockRuleSetTwo = new Mock<IPasswordRuleSet>();
            mockRuleSetTwo.SetupGet(x => x.RuleSetId).Returns(2).Verifiable();
            mockRuleSetTwo.SetupGet(x => x.RuleSetMessage).Returns("RuleSetTwo");

            var password = new Core.Password("hello");

            mockRuleOne.Setup(x => x.IsValid(password)).Returns(true).Verifiable();
            mockRuleTwo.Setup(x => x.IsValid(password)).Returns(false).Verifiable();
            mockRuleSetOne.Setup(x => x.IsValid(It.IsAny<Dictionary<int, bool>>())).Returns(true).Verifiable();
            mockRuleSetTwo.Setup(x => x.IsValid(It.IsAny<Dictionary<int, bool>>())).Returns(false).Verifiable();


            var sut = new RuleEngine();
            sut.AddRule(mockRuleOne.Object)
                .AddRule(mockRuleTwo.Object)
                .AddRuleSet(mockRuleSetOne.Object)
                .AddRuleSet(mockRuleSetTwo.Object);

            // Act
            var result = sut.Evaluate(password);

            // Assert
            result.IsVerifiedOkay.Should().BeFalse();
            result.RuleResults.Should().HaveCount(2);
            result.RuleSetResults.Should().HaveCount(2);
            mockRuleOne.VerifyAll();
            mockRuleTwo.VerifyAll();
            mockRuleSetOne.VerifyAll();
            mockRuleSetTwo.VerifyAll();
        }

        [Fact()]
        public void Evaluate_Should_VerifyPasswordAsOkay_WhenRulesAndRuleSetsAreConfiguredAndAllRuleSetPasses()
        {
            // Arrange
            var mockRuleOne = new Mock<IPasswordRule>();
            mockRuleOne.SetupGet(x => x.RuleId).Returns(1).Verifiable();
            mockRuleOne.SetupGet(x => x.RuleMessage).Returns("RuleOne").Verifiable();
            var mockRuleTwo = new Mock<IPasswordRule>();
            mockRuleTwo.SetupGet(x => x.RuleId).Returns(2).Verifiable();
            mockRuleTwo.SetupGet(x => x.RuleMessage).Returns("RuleTwo").Verifiable();

            var mockRuleSetOne = new Mock<IPasswordRuleSet>();
            mockRuleSetOne.SetupGet(x => x.RuleSetId).Returns(1).Verifiable();
            mockRuleSetOne.SetupGet(x => x.RuleSetMessage).Returns("RuleSetOne").Verifiable();
            var mockRuleSetTwo = new Mock<IPasswordRuleSet>();
            mockRuleSetTwo.SetupGet(x => x.RuleSetId).Returns(2).Verifiable();
            mockRuleSetTwo.SetupGet(x => x.RuleSetMessage).Returns("RuleSetTwo");

            var password = new Core.Password("hello");

            mockRuleOne.Setup(x => x.IsValid(password)).Returns(true).Verifiable();
            mockRuleTwo.Setup(x => x.IsValid(password)).Returns(false).Verifiable();
            mockRuleSetOne.Setup(x => x.IsValid(It.IsAny<Dictionary<int, bool>>())).Returns(true).Verifiable();
            mockRuleSetTwo.Setup(x => x.IsValid(It.IsAny<Dictionary<int, bool>>())).Returns(true).Verifiable();


            var sut = new RuleEngine();
            sut.AddRule(mockRuleOne.Object)
                .AddRule(mockRuleTwo.Object)
                .AddRuleSet(mockRuleSetOne.Object)
                .AddRuleSet(mockRuleSetTwo.Object);

            // Act
            var result = sut.Evaluate(password);

            // Assert
            result.IsVerifiedOkay.Should().BeTrue();
            result.RuleResults.Should().HaveCount(2);
            result.RuleSetResults.Should().HaveCount(2);
            mockRuleOne.VerifyAll();
            mockRuleTwo.VerifyAll();
            mockRuleSetOne.VerifyAll();
            mockRuleSetTwo.VerifyAll();
        }
    }
}
