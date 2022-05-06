using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract.Exceptions
{
    [Serializable]
    public class FeatureTrackingComponentException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FeatureTrackingComponentException()
        {
        }

        public FeatureTrackingComponentException(string message) : base(message)
        {
        }

        public FeatureTrackingComponentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FeatureTrackingComponentException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
