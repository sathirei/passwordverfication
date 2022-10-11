using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Password.Core.Tests")]
namespace Password.Core
{
    public class Password
    {
        private readonly string value;
        public Password(string password) => value = password;

        public bool Evaluate(Func<string, bool> evaluationFunction)
        {
            return evaluationFunction(value);
        }

    }
}