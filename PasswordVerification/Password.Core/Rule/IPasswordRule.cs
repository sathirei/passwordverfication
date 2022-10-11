namespace Password.Core.Rule
{
    public interface IPasswordRule
    {
        public int RuleId { get; }
        public int Order { get; }
        public string RuleMessage { get; }

        public bool IsValid(Password password);
    }
}
