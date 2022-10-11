namespace Password.RuleSet
{
    public class MandatoryRuleSet : BasePasswordRuleSet
    {
        private readonly List<int>? _mandatoryRuleIds;
        public MandatoryRuleSet(int ruleSetId, List<int> mandatoryRuleIds)
            : base(ruleSetId,
                  $"All mandatory password rules should be satisfied, mandatory rules are {string.Join(',', mandatoryRuleIds ?? new List<int>())}")
            => _mandatoryRuleIds = mandatoryRuleIds;
        public override bool IsValid(IDictionary<int, bool> ruleResults)
        {
            if (_mandatoryRuleIds != null)
            {
                foreach (var ruleId in _mandatoryRuleIds)
                {
                    if (!ruleResults.TryGetValue(ruleId, out var ruleResult) || !ruleResult)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
