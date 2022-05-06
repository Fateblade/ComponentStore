using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.ConsentManager.Contract.Exceptions
{
    [Serializable]
    public class ConsentManagementException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConsentManagementException()
        {
        }

        public ConsentManagementException(string message) : base(message)
        {
        }

        public ConsentManagementException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConsentManagementException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
