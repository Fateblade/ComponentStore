using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions
{
    [Serializable]
    public class ViewModelMapperException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ViewModelMapperException()
        {
        }

        public ViewModelMapperException(string message) : base(message)
        {
        }

        public ViewModelMapperException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ViewModelMapperException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

}
