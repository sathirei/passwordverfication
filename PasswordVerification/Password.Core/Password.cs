using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Password.Core.Tests")]
namespace Password.Core
{
    public class Password
    {
        private readonly string _value;
        public Password(string password) => _value = password;

        public bool Evaluate(Func<string, bool> evaluationFunction)
        {
            return evaluationFunction(_value);
        }

    }
}