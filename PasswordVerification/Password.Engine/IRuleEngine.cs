using Password.Core.Rule;
using Password.RuleSet;

namespace Password.Engine
{
    public interface IRuleEngine
    {
        public RuleEngine AddRule(IPasswordRule rule);
        public RuleEngine AddRuleSet(IPasswordRuleSet ruleSet);

        public PasswordVerificationResult Evaluate(Core.Password password);
    }
}
