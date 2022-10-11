using System.Runtime.Serialization;

namespace Password.Engine.Exceptions
{
    [Serializable]
    public class AddRuleSetException : Exception
    {
        public AddRuleSetException(string message) : base(message)
        {

        }
        protected AddRuleSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
