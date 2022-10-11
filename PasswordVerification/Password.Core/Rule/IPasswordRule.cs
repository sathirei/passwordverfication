namespace Password.Core.Rule
{
    public interface IPasswordRule
    {
        public int Order { get; }
        public int RuleMessage { get; }

        public bool IsValid(Password password);
    }
}
