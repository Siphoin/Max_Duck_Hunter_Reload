using System;

namespace Exception
{

    [Serializable]
    public class InteractionObjectNotContainsOnMemoryException : System.Exception
    {
        public InteractionObjectNotContainsOnMemoryException() { }
        public InteractionObjectNotContainsOnMemoryException(string message) : base(message) { }
        public InteractionObjectNotContainsOnMemoryException(string message, System.Exception inner) : base(message, inner) { }
        protected InteractionObjectNotContainsOnMemoryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
