using System.Collections;

namespace Password.Core.Rule
{
    public interface IPasswordRule: IEqualityComparer
    {
        public int RuleId { get; }
        public int Order { get; }
        public string RuleMessage { get; }

        public bool IsValid(Password password);
    }
}
