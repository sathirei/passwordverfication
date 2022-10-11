using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Password.Core.Tests")]
namespace Password.Core
{
    public class Password
    {
        private readonly string Value;
        public Password(string password) => Value = password;

        public bool Evaluate(Func<string, bool> evaluationFunction)
        {
            return evaluationFunction(Value);
        }

    }
}