using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions
{
    [Serializable]
    public class ViewModelMappingAlreadyExistsException : ViewModelMapperException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ViewModelMappingAlreadyExistsException()
        {
        }

        public ViewModelMappingAlreadyExistsException(string message) : base(message)
        {
        }

        public ViewModelMappingAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ViewModelMappingAlreadyExistsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
