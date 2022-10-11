using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Password.Core.Tests")]
namespace Password.Core
{
    public class Password
    {
        internal string Value { get; }
        public Password(string password) => Value = password;

    }
}