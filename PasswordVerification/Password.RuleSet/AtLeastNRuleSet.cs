namespace Password.RuleSet
{
    public class AtLeastNRuleSet : BasePasswordRuleSet
    {
        private readonly int _validRuleCount;
        public AtLeastNRuleSet(int ruleSetId, int ruleCount)
            : base(ruleSetId, $"At least {ruleCount} password rule(s) should be satisfied.") => _validRuleCount = ruleCount;

        public override bool IsValid(IDictionary<int, bool> ruleResults)
        {
            return ruleResults.Select(x => x.Value).Count() > _validRuleCount;
        }
    }
}
