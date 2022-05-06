using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Data.ApplicationBase.DataStoring.Contract.Exceptions
{
    [Serializable]
    public class ApplicationBaseDataStoringException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ApplicationBaseDataStoringException()
        {
        }

        public ApplicationBaseDataStoringException(string message) : base(message)
        {
        }

        public ApplicationBaseDataStoringException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ApplicationBaseDataStoringException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
