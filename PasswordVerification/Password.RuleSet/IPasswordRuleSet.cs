namespace Password.RuleSet
{
    public interface IPasswordRuleSet
    {
        public int RuleSetId { get; }

        public bool IsValid(IDictionary<int, bool> ruleResults);
    }
}
