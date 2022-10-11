using Password.Core.Rule;
using Password.Engine.Exceptions;
using Password.RuleSet;
using System.Collections.Concurrent;

namespace Password.Engine
{
    public class RuleEngine : IRuleEngine
    {
        private readonly ConcurrentDictionary<int, IPasswordRule> rules;
        private readonly ConcurrentDictionary<int, IPasswordRuleSet> ruleSets;
        public RuleEngine()
        {
            rules = new ConcurrentDictionary<int, IPasswordRule>();
            ruleSets = new ConcurrentDictionary<int, IPasswordRuleSet>();
        }

        public RuleEngine AddRule(IPasswordRule rule)
        {
            if (rules.TryAdd(rule.RuleId, rule))
            {
                return this;
            }
            throw new AddRuleException($"Adding to rules failed for rule with ID: {rule.RuleId}, Rule: {rule.RuleMessage}");
        }

        public RuleEngine AddRuleSet(IPasswordRuleSet ruleSet)
        {
            if (ruleSets.TryAdd(ruleSet.RuleSetId, ruleSet))
            {
                return this;
            }
            throw new AddRuleSetException($"Adding to rule sets failed for rule set with ID: {ruleSet.RuleSetId}, RuleSet: {ruleSet?.GetType().Name}");
        }

        public PasswordVerificationResult Evaluate(Core.Password password)
        {
            var ruleResults = rules.AsParallel()
                .Select(x => new 
                { 
                    RuleId = x.Key, 
                    Result = x.Value.IsValid(password)
                }).ToList();

            var ruleSetResults = ruleSets.AsParallel()
                .Select(x => new 
                { 
                    RuleSetId = x.Key,
                    Result = x.Value.IsValid(ruleResults.ToDictionary(x => x.RuleId, y => y.Result))
                }).ToList();

            return new PasswordVerificationResult(
                ruleResults!.Select(x => new Result(
                    x!.RuleId,
                    x!.Result, rules[x!.RuleId]!.RuleMessage!)
                )!.ToList(),
                ruleSetResults!.Select(x => new Result(
                    x!.RuleSetId,
                    x!.Result,
                    ruleSets[x!.RuleSetId]!.RuleSetMessage!))!.ToList());
        }
    }
}