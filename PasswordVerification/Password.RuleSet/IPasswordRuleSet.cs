using System.Collections;

namespace Password.RuleSet
{
    public interface IPasswordRuleSet : IEqualityComparer
    {
        public int RuleSetId { get; }
        public string RuleSetMessage { get; }

        bool Equals(object obj);
        public bool IsValid(IDictionary<int, bool> ruleResults);
    }
}
