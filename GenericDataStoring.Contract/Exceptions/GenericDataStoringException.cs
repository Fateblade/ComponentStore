using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Data.GenericDataStoring.Contract.Exceptions
{
    [Serializable]
    public class GenericDataStoringException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public GenericDataStoringException()
        {
        }

        public GenericDataStoringException(string message) : base(message)
        {
        }

        public GenericDataStoringException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GenericDataStoringException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
