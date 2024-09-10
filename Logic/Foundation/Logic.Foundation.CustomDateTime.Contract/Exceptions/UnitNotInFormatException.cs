using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class UnitNotInFormatException : CustomDateTimeException
    {
        public UnitNotInFormatException(DateTimeUnit unit) : base($"Unit '{unit.FullName}' ({unit.Id}) not in format of time machine")
        {
        }
    }
}
