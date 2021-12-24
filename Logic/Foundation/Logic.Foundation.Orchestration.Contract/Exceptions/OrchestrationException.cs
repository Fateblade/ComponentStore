using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.Orchestration.Contract.Exceptions
{
    [Serializable]
    public class OrchestrationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public OrchestrationException()
        {
        }

        public OrchestrationException(string message) : base(message)
        {
        }

        public OrchestrationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OrchestrationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
