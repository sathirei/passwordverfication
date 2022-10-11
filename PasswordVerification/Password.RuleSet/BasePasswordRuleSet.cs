namespace Password.RuleSet
{
    public abstract class BasePasswordRuleSet : IPasswordRuleSet
    {
        public int RuleSetId { get; }

        public string RuleSetMessage { get; }

        protected BasePasswordRuleSet(int ruleSetId, string ruleSetMessage)
        {
            RuleSetId = ruleSetId;
            RuleSetMessage = ruleSetMessage;
        }

        public abstract bool IsValid(IDictionary<int, bool> ruleResults);
        public new bool Equals(object? x, object? y)
        {
            var lhs = (x as IPasswordRuleSet);
            var rhs = (y as IPasswordRuleSet);
            if (lhs != null && rhs != null)
            {
                return lhs.RuleSetId == rhs.RuleSetId;
            }
            return false;
        }

        public int GetHashCode(object obj)
        {
            return this.RuleSetId;
        }
    }
}
