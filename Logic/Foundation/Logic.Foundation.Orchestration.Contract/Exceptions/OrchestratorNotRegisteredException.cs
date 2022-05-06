using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.Orchestration.Contract.Exceptions
{
    [Serializable]
    public class OrchestratorNotRegisteredException : OrchestrationException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public OrchestratorNotRegisteredException()
        {
        }

        public OrchestratorNotRegisteredException(string message) : base(message)
        {
        }

        public OrchestratorNotRegisteredException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OrchestratorNotRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
