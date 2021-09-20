using System;

namespace Exception
{
    [Serializable]
    public class InteractionObjectContainsOnMemoryException : System.Exception
    {
        public InteractionObjectContainsOnMemoryException() { }
        public InteractionObjectContainsOnMemoryException(string message) : base(message) { }
        public InteractionObjectContainsOnMemoryException(string message, System.Exception inner) : base(message, inner) { }
        protected InteractionObjectContainsOnMemoryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
