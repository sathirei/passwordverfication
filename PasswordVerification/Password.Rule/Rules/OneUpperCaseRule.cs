using Password.Core.Rule;

namespace Password.Rule.Rules
{
    public class OneUpperCaseRule : IPasswordRule
    {
        public int Order { get; }

        public string RuleMessage { get; }
        public OneUpperCaseRule(int order, string ruleMessage)
        {
            this.Order = order;
            this.RuleMessage = ruleMessage;
        }

        public bool IsValid(Core.Password password)
        {
            return password.Evaluate(s => (s != null) && s.Any(char.IsUpper));
        }
    }
}
