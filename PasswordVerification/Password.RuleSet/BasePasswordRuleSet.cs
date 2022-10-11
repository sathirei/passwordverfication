namespace Password.RuleSet
{
    public abstract class BasePasswordRuleSet : IPasswordRuleSet
    {
        public int RuleSetId { get; }
        protected BasePasswordRuleSet(int ruleSetId) => RuleSetId = ruleSetId;

        public abstract bool IsValid(IDictionary<int, bool> ruleResults);
    }
}
