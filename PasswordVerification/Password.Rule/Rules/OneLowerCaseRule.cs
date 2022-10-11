namespace Password.Rule.Rules
{
    public class OneLowerCaseRule : BasePasswordRule
    {
        public OneLowerCaseRule(int ruleId, int order, string ruleMessage)
            : base(ruleId, order, ruleMessage)
        { }

        public override bool IsValid(Core.Password password)
        {
            return password.Evaluate(s => (s != null) && s.Any(char.IsLower));
        }
    }
}
