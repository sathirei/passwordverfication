namespace Password.Rule.Rules
{
    public class OneDigitRule : BasePasswordRule
    {
        public OneDigitRule(int ruleId, int order, string ruleMessage)
            : base(ruleId, order, ruleMessage)
        { }

        public override bool IsValid(Core.Password password)
        {
            return password.Evaluate(s => (s != null) && s.Any(char.IsDigit));
        }
    }
}
