using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.ApplicationBaseManager.Contract.Exceptions
{
    [Serializable]
    public class ApplicationBaseManagerException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ApplicationBaseManagerException()
        {
        }

        public ApplicationBaseManagerException(string message) : base(message)
        {
        }

        public ApplicationBaseManagerException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ApplicationBaseManagerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
