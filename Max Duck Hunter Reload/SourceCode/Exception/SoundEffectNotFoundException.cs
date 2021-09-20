using System;

namespace Exception
{

    [Serializable]
    public class SoundEffectNotFoundException : System.Exception
    {
        public SoundEffectNotFoundException() { }
        public SoundEffectNotFoundException(string message) : base(message) { }
        public SoundEffectNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected SoundEffectNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
