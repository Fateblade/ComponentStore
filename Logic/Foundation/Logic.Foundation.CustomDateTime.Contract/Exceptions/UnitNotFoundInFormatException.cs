using System;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class UnitNotFoundInFormatException : DateTimeFormatException
    {
        public UnitNotFoundInFormatException(DateTimeUnit unit) :
            base($"'{unit.FullName}' ({unit.Id}) not found in time format")
        {
        }
    }

}