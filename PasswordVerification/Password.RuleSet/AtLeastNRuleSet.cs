namespace Password.RuleSet
{
    public class AtLeastNRuleSet : BasePasswordRuleSet
    {
        private readonly int validRuleCount;
        public AtLeastNRuleSet(int ruleSetId, int ruleCount) : base(ruleSetId) => validRuleCount = ruleCount;

        public override bool IsValid(IDictionary<int, bool> ruleResults)
        {
            return ruleResults.Select(x => x.Value).Count() > validRuleCount;
        }
    }
}
