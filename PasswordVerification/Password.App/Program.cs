// See https://aka.ms/new-console-template for more information
using Password.Engine;
using Password.Rule.Rules;
using Password.RuleSet;

object _messageLock = new object();

var ruleEngine = new RuleEngine();
ConfigureRuleEngine(ruleEngine);

do
{
    Console.WriteLine("Please enter your password:"); // For simplicity we are printing the password back to the console however we can mask it
    var password = Console.ReadLine();
    var result = ruleEngine.Evaluate(new Password.Core.Password(password));
    Console.WriteLine();
    WriteVerificationResult(_messageLock, result);
} while (true);

void ConfigureRuleEngine(RuleEngine ruleEngine)
{
    AddRules(ruleEngine);
    AddRuleSets(ruleEngine);
}

void AddRules(RuleEngine ruleEngine)
{
    var moreThanEightCharactersRule = new MoreThanEightCharactersRule(1, 1, "Password should be more than 8 characters in length.");
    var notNullRule = new NotNullRule(2, 2, "Password should be provided.");
    var oneDigitRule = new OneDigitRule(3, 3, "Password should contain at least one digit.");
    var oneLowerCaseRule = new OneLowerCaseRule(4, 4, "Password should contain at least one lower case.");
    var oneUpperCaseRule = new OneUpperCaseRule(5, 5, "Password should contain at least one lower case.");

    ruleEngine.AddRule(moreThanEightCharactersRule)
        .AddRule(notNullRule)
        .AddRule(oneDigitRule)
        .AddRule(oneLowerCaseRule)
        .AddRule(oneUpperCaseRule);
}

void AddRuleSets(RuleEngine ruleEngine)
{
    var atleastThreeRuleSet = new AtLeastNRuleSet(1, 3);
    var mandatoryRuleSet = new MandatoryRuleSet(2, new List<int> { 1, 2, 3, 4, 5 });

    ruleEngine.AddRuleSet(atleastThreeRuleSet)
        .AddRuleSet(mandatoryRuleSet);
}

void WriteColoredMessage(string message, bool result)
{
    lock (_messageLock)
    {
        Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

void WriteVerificationResult(object _messageLock, PasswordVerificationResult? result)
{
    WriteColoredMessage(result.IsVerifiedOkay ? "OK" : "Verification Failed", result.IsVerifiedOkay);
    Console.WriteLine();
    Console.WriteLine("Rules:");
    result?.RuleResults?.OrderBy(x => x.Id).ToList().ForEach(x => WriteColoredMessage($"{x.Id}. {x.Message}", x.IsPass));
    Console.WriteLine();
    Console.WriteLine("RuleSets:");
    result?.RuleSetResults?.OrderBy(x => x.Id).ToList().ForEach(x => WriteColoredMessage($"{x.Id}. {x.Message}", x.IsPass));
    Console.WriteLine();
}