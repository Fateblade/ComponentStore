using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.Orchestration.Contract.Exceptions
{
    [Serializable]
    public class OrchestratorAlreadyRegisteredException : OrchestrationException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public OrchestratorAlreadyRegisteredException()
        {
        }

        public OrchestratorAlreadyRegisteredException(string message) : base(message)
        {
        }

        public OrchestratorAlreadyRegisteredException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OrchestratorAlreadyRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
