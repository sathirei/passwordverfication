namespace Password.Rule.Rules
{
    public class MoreThanEightCharactersRule : BasePasswordRule
    {
        public MoreThanEightCharactersRule(int ruleId, int order, string ruleMessage)
            :base(ruleId, order, ruleMessage)
        { }

        public override bool IsValid(Core.Password password)
        {
            return password.Evaluate(s => s?.Length > 8);
        }
    }
}
