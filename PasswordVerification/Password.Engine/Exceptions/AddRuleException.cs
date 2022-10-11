using System.Runtime.Serialization;

namespace Password.Engine.Exceptions
{
    [Serializable]
    public class AddRuleException : Exception
    {
        public AddRuleException(string message) : base(message)
        {

        }
        protected AddRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
