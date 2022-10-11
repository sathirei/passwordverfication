using Password.Core.Rule;

namespace Password.Rule.Rules
{
    public abstract class BasePasswordRule : IPasswordRule
    {
        public int Order { get; }

        public string RuleMessage { get; }

        public int RuleId { get; }

        protected BasePasswordRule(int ruleId, int order, string ruleMessage)
        {
            this.RuleId = ruleId;
            this.Order = order;
            this.RuleMessage = ruleMessage;
        }

        public abstract bool IsValid(Core.Password password);

        public new bool Equals(object? x, object? y)
        {
            var lhs = (x as IPasswordRule);
            var rhs = (y as IPasswordRule);
            if (lhs != null && rhs != null)
            {
                return lhs.RuleId == rhs.RuleId;
            }
            return false;
        }

        public int GetHashCode(object obj)
        {
            return this.RuleId;
        }
    }
}
