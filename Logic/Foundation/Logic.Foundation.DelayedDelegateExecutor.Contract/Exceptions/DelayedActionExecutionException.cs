using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.DelayedActionExecution.Contract.Exceptions
{
    [Serializable]
    public class DelayedActionExecutionException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DelayedActionExecutionException()
        {
        }

        public DelayedActionExecutionException(string message) : base(message)
        {
        }

        public DelayedActionExecutionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DelayedActionExecutionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
