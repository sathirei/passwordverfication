using System.Collections.ObjectModel;

namespace Password.Engine
{
    public class PasswordVerificationResult
    {
        public ReadOnlyCollection<Result>? RuleResults { get; }
        public ReadOnlyCollection<Result>? RuleSetResults { get; }
        public bool IsVerifiedOkay =>
            AllRuleSetsPass() ||
            NoRuleSetsConfiguredAndAllRulesPass() ||
            NoRulesOrRuleSetsConfigured();

        public PasswordVerificationResult(List<Result> ruleResults, List<Result> ruleSetResults)
        {
            RuleResults = ruleResults?.AsReadOnly();
            RuleSetResults = ruleSetResults?.AsReadOnly();
        }

        private bool AllRuleSetsPass()
        {
            return (RuleSetResults != null && RuleSetResults.Count != 0) && RuleSetResults!.All(x => x.IsPass);
        }

        private bool NoRuleSetsConfiguredAndAllRulesPass()
        {
            return (RuleSetResults == null || RuleSetResults.Count == 0) && (RuleResults != null && RuleResults.Count != 0) && RuleResults!.All(x => x.IsPass);
        }

        private bool NoRulesOrRuleSetsConfigured()
        {
            return (RuleSetResults == null || RuleSetResults.Count == 0) && (RuleResults == null || RuleResults.Count == 0);
        }
    }
}
