namespace Password.RuleSet
{
    public class MandatoryRuleSet : BasePasswordRuleSet
    {
        private readonly List<int> mandatoryRuleIds;
        public MandatoryRuleSet(int ruleSetId, List<int> mandatoryRulesId) : base(ruleSetId) => mandatoryRuleIds = mandatoryRulesId;
        public override bool IsValid(IDictionary<int, bool> ruleResults)
        {
            foreach (var ruleId in mandatoryRuleIds)
            {
                if (!ruleResults.TryGetValue(ruleId, out var ruleResult) || !ruleResult)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
