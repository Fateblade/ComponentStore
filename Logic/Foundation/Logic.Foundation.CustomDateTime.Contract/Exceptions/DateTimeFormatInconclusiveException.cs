using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class DateTimeFormatInconclusiveException : DateTimeFormatException
    {
        public DateTimeFormatInconclusiveException(DateTimeUnit unitWithInconclusiveValue, DateTimeUnitRelation relation, ulong value, ulong existingValue)
        : base($"'{unitWithInconclusiveValue.FullName}' ({unitWithInconclusiveValue.Id}) has already been calculated to a value of {existingValue}, but relation '{relation.Name}' ({relation.Id}) results in a differing value {value}")
        {
        }
    }
}
