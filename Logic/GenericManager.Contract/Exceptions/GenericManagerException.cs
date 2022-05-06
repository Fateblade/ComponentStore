using System;

namespace Fateblade.Components.Logic.GenericManager.Contract.Exceptions
{
    [Serializable]
    public class GenericManagerException : Exception
    {
        public GenericManagerException() { }
        public GenericManagerException(string message) : base(message) { }
        public GenericManagerException(string message, Exception inner) : base(message, inner) { }
        protected GenericManagerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
