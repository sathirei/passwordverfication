namespace Password.Rule.Rules
{
    public class OneUpperCaseRule : BasePasswordRule
    {
        public OneUpperCaseRule(int ruleId, int order, string ruleMessage)
            : base(ruleId, order, ruleMessage)
        { }

        public override bool IsValid(Core.Password password)
        {
            return password.Evaluate(s => (s != null) && s.Any(char.IsUpper));
        }
    }
}
