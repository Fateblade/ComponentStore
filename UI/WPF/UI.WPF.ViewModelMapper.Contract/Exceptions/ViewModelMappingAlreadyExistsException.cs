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

        public ViewModelMappingAlreadyExistsException(Type viewModelTypeOfAlreadyExistingMapping)
        : base ($"Mapping already exists for view model of type '{viewModelTypeOfAlreadyExistingMapping}'")
        { }

        protected ViewModelMappingAlreadyExistsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
